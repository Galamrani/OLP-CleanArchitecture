import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { Location } from '@angular/common';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../services/toast.service';

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toast = inject(ToastService);
  const location = inject(Location);

  const messages: Record<number, string> = {
    400: 'Invalid request',
    401: 'You are not authorized',
    403: 'Access forbidden',
    404: 'Resource not found',
    500: 'Server error',
  };

  const redirects: Record<number, () => void> = {
    400: () => location.back(),
    401: () => router.navigate(['/login']),
    403: () => location.back(),
    404: () => router.navigate(['/not-found']),
    500: () => router.navigate(['/server-error']),
  };

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const status = error.status;

      const errorMessage = messages[status] || `Unexpected error (status: ${status}).`;
      toast.showError(errorMessage);

      // Delay navigation so the toast has time to appear
      const redirect = redirects[status];
      if (redirect) {
        setTimeout(() => redirect(), 1500); // Adjust delay (in ms) as needed
      }

      return throwError(() => error);
    })
  );
};
