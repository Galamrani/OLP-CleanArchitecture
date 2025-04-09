import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ViewStore } from '../stores/view.store';
import { CourseViewType } from '../models/user-view.enum';
import { ToastService } from '../services/toast.service';

export const viewGuard: CanActivateFn = (route, state) => {
  const viewStore = inject(ViewStore);
  const router = inject(Router);
  const toast = inject(ToastService);

  const currentUrl = state.url;

  if (viewStore.view() !== CourseViewType.Instructor && currentUrl.includes('/courses/instructor')) {
    toast.showError('You are not allowed, invalid view');
    router.navigate(['/home']);
    return false;
  }

  if (viewStore.view() !== CourseViewType.Student && currentUrl.includes('/courses/student')) {
    toast.showError('You are not allowed, invalid view');
    router.navigate(['/home']);
    return false;
  }

  return true;
};
