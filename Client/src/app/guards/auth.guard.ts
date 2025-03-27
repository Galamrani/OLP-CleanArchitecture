import { CanActivateFn, Router } from '@angular/router';
import { UserStore } from '../stores/user.store';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const userStore = inject(UserStore);
  const router = inject(Router);
  const toastr = inject(ToastrService);

  if (!userStore.isLoggedIn()) {
    toastr.error('You are not allowed, need to login');
    router.navigate(['/login']);
    return false;
  }

  return true;
};
