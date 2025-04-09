import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from '../../../models/user.model';
import { CommonModule } from '@angular/common';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';
import { AuthService } from '../../../services/auth.service';
import { PasswordValidationUtils } from '../../../utils/password-validation.utils';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent {
  userForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private viewStore: ViewStore,
    private router: Router,
    private toast: ToastService
  ) { }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(/[A-Z]/), // At least one uppercase letter
          Validators.pattern(/\d/), // At least one digit
          Validators.pattern(/[\W_]/), // At least one special character
        ],
      ],
    });
  }

  send() {
    if (this.userForm.invalid) return;

    const user: UserModel = this.userForm.value;
    this.authService.register(user).subscribe({
      next: () => {
        this.toast.showSuccess(`Registration Successful, Welcome, ${user.name || 'User'}!`);
        this.viewStore.setView(CourseViewType.Student);
        this.router.navigateByUrl('/courses/student');
      },
      error: () => this.toast.showError('Register failed!'),
    });
  }

  hasUppercaseError() {
    return PasswordValidationUtils.hasUppercaseError(this.userForm);
  }

  hasDigitError() {
    return PasswordValidationUtils.hasDigitError(this.userForm);
  }

  hasSpecialCharError() {
    return PasswordValidationUtils.hasSpecialCharError(this.userForm);
  }
}
