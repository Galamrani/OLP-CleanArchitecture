import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { CredentialsModel } from '../../../models/credentials.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';

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
    private userService: UserService,
    private viewStore: ViewStore,
    private router: Router,
    private toastr: ToastrService
  ) {}

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

  async send() {
    if (this.credentialsForm.invalid) return;

    try {
      const credentials: CredentialsModel = this.credentialsForm.value;
      await this.userService.login(credentials);
      this.toastr.success('Welcome back!');
      this.viewStore.setView(CourseViewType.Student);
      this.router.navigateByUrl('/courses/student');
    } catch (err: any) {
      this.toastr.error('Login failed!');
    }
  }

  // Helper methods to check specific errors
  hasUppercaseError() {
    return (
      this.credentialsForm.get('password')?.hasError('pattern') &&
      !/[A-Z]/.test(this.credentialsForm.get('password')?.value)
    );
  }

  hasDigitError() {
    return (
      this.credentialsForm.get('password')?.hasError('pattern') &&
      !/\d/.test(this.credentialsForm.get('password')?.value)
    );
  }

  hasSpecialCharError() {
    return (
      this.credentialsForm.get('password')?.hasError('pattern') &&
      !/[\W_]/.test(this.credentialsForm.get('password')?.value)
    );
  }
}
