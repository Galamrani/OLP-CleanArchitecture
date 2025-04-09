import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CredentialsModel } from '../../../models/credentials.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';
import { AuthService } from '../../../services/auth.service';
import { PasswordValidationUtils } from '../../../utils/password-validation.utils';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit {

  credentialsForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private viewStore: ViewStore,
    private router: Router,
    private toast: ToastService
  ) { }

  ngOnInit() {
    this.credentialsForm = this.formBuilder.group({
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
    if (this.credentialsForm.invalid) return;

    const credentials: CredentialsModel = this.credentialsForm.value;
    this.authService.login(credentials).subscribe({
      next: () => {
        this.toast.showSuccess('Welcome back!');
        this.viewStore.setView(CourseViewType.Student);
        this.router.navigateByUrl('/courses/student');
      },
      error: () => this.toast.showError('Login failed!'),
    });
  }

  hasUppercaseError() {
    return PasswordValidationUtils.hasUppercaseError(this.credentialsForm);
  }

  hasDigitError() {
    return PasswordValidationUtils.hasDigitError(this.credentialsForm);
  }

  hasSpecialCharError() {
    return PasswordValidationUtils.hasSpecialCharError(this.credentialsForm);
  }
}
