import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError, EMPTY } from 'rxjs';
import { ErrorUtils } from '../utils/error-utils';

/**
 * **HTTP Error Interceptor**
 * errorInterceptor only executes during the response phase when an error occurs. It does not affect requests or successful responses.
 * This interceptor handles HTTP errors globally, preventing the need for repetitive
 * error handling in services and components. It categorizes errors based on their
 * status codes and applies appropriate actions:
 *
 * - **400 (Bad Request)** → Logs validation or client-side errors.
 * - **401 (Unauthorized)** → Logs unauthorized access attempts.
 * - **404 (Not Found)** → Redirects to a "Not Found" page.
 * - **500 (Server Error)** → Redirects to a "Server Error" page with details.
 * - **Other Errors** → Logs unknown errors for debugging.
 *
 * If an error is handled, further processing of the request is stopped using `EMPTY`.
 * Otherwise, the error is re-thrown for potential handling elsewhere.
 */

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error) {
        let shouldStop = false; // Flag to control stopping other interceptors

        switch (error.status) {
          case 400:
            console.log('Bad Request' + '-->' + ErrorUtils.parseError(error));
            shouldStop = true;
            break;

          case 401:
            console.log('Unauthorized' + ' --> ' + error.message);
            shouldStop = true;
            break;

          case 404:
            console.log('Not-found' + ' --> ' + error.message);
            router.navigate(['not-found']);
            shouldStop = true;
            break;

          case 500:
            console.log('Server-error' + ' --> ' + error.message);
            router.navigate(['server-error'], {
              state: {
                errorMessage: error.message,
                errorDetails: ErrorUtils.parseError(error),
              },
            });
            shouldStop = true;
            break;

          default:
            console.log('Unknown error' + ' --> ' + error.message);
            shouldStop = true;
            break;
        }

        if (shouldStop) {
          return EMPTY;
        }
      }

      return throwError(() => error); // Allow error propagation if needed
    })
  );
};
