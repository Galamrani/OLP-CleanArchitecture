import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError, EMPTY } from "rxjs";
import { ErrorUtils } from "../utils/error-utils";

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
        let parsedMessage = "";
        switch (error.status) {
          case 400:
            parsedMessage = ErrorUtils.parseError(error);
            break;
          case 401:
            parsedMessage = ErrorUtils.parseError(error);
            break;
          case 404:
            router.navigate(["not-found"], {
              state: {
                errorMessage: error.message,
                errorDetails: ErrorUtils.parseError(error),
              },
            });
            return EMPTY;
          case 500:
            router.navigate(["server-error"], {
              state: {
                errorMessage: error.message,
                errorDetails: ErrorUtils.parseError(error),
              },
            });
            return EMPTY;
          default:
            break;
        }

        // Wrap the original error instead of cloning it, and then Re-throw it
        return throwError(() => ({
          ...error,
          parsedMessage,
        }));
      }
      // Re-throw the original
      return throwError(() => error);
    })
  );
};
