import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class ToastService {
  constructor(private toastr: ToastrService) { }

  showError(message: string): void {
    this.toastr.error(message);
  }

  showSuccess(message: string): void {
    this.toastr.success(message);
  }

  showInfo(message: string): void {
    this.toastr.info(message);
  }
}


