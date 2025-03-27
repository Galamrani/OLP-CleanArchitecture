import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ViewStore } from '../stores/view.store';
import { CourseViewType } from '../models/user-view.enum';
import { ToastrService } from 'ngx-toastr';

export const viewGuard: CanActivateFn = (route, state) => {
  const viewStore = inject(ViewStore);
  const router = inject(Router);
  const toastr = inject(ToastrService);

  const currentUrl = state.url;

  if (
    viewStore.view() !== CourseViewType.Instructor &&
    currentUrl.includes('/courses/instructor')
  ) {
    toastr.error('You are not allowed, invalid view');
    router.navigate(['/home']);
    return false;
  }

  if (
    viewStore.view() !== CourseViewType.Student &&
    currentUrl.includes('/courses/student')
  ) {
    toastr.error('You are not allowed, invalid view');
    router.navigate(['/home']);
    return false;
  }

  return true;
};
