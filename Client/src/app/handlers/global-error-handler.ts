import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ToastService } from '../services/toast.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
    constructor(
        private zone: NgZone,
        private injector: Injector,
    ) { }

    handleError(error: Error | HttpErrorResponse): void {
        // Use NgZone to ensure Angular's change detection is triggered
        // Errors may occur outside Angular's zone (in setTimeout, Promise callbacks, or third-party libraries)
        // which would prevent UI updates like toast notifications from appearing immediately.
        // By wrapping our error handling logic in zone.run(), we force Angular to run change detection
        // after handling the error, ensuring toast messages appear and UI updates properly.
        this.zone.run(() => {
            if (!(error instanceof HttpErrorResponse)) {
                if (!environment.production) {
                    console.error('Client error occurred:', error);
                }

                // Manually resolve ToastService here to avoid DI cycle
                const toast = this.injector.get(ToastService);
                toast.showError('An unexpected error occurred.');
            }
        });
    }
}