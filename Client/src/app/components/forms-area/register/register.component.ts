import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { Router } from '@angular/router';
import { UserModel } from '../../../models/user.model';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';

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
    private userService: UserService,
    private viewStore: ViewStore,
    private router: Router,
    private toastr: ToastrService
  ) {}

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

  async send() {
    if (this.userForm.invalid) return;

    try {
      const user: UserModel = this.userForm.value;
      await this.userService.register(user);
      this.toastr.success(
        `Welcome, ${user.name || 'User'}!`,
        'Registration Successful'
      );
      this.viewStore.setView(CourseViewType.Student);
      this.router.navigateByUrl('/courses/student');
    } catch (err: any) {
      this.toastr.error('Register failed!');
    }
  }

  // Validation Helper Methods
  hasUppercaseError() {
    return (
      this.userForm.get('password')?.hasError('pattern') &&
      !/[A-Z]/.test(this.userForm.get('password')?.value)
    );
  }

  hasDigitError() {
    return (
      this.userForm.get('password')?.hasError('pattern') &&
      !/\d/.test(this.userForm.get('password')?.value)
    );
  }

  hasSpecialCharError() {
    return (
      this.userForm.get('password')?.hasError('pattern') &&
      !/[\W_]/.test(this.userForm.get('password')?.value)
    );
  }
}
