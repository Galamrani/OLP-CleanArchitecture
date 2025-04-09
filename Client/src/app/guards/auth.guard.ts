import { CanActivateFn, Router } from '@angular/router';
import { UserStore } from '../stores/user.store';
import { inject } from '@angular/core';
import { ToastService } from '../services/toast.service';

export const authGuard: CanActivateFn = (route, state) => {
  const userStore = inject(UserStore);
  const router = inject(Router);
  const toast = inject(ToastService);

  if (!userStore.isLoggedIn()) {
    toast.showError('You are not allowed, need to login');
    router.navigate(['/login']);
    return false;
  }

  return true;
};
