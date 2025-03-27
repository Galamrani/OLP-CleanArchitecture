import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CourseModel } from '../../../models/course.model';
import { UserStore } from '../../../stores/user.store';
import { CourseManagerService } from '../../../services/course-manager.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-course',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-course.component.html',
  styleUrl: './add-course.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddCourseComponent implements OnInit {
  courseForm!: FormGroup;

  private userStore = inject(UserStore);

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private CourseManagerService: CourseManagerService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.courseForm = this.formBuilder.group({
      title: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(200),
        ],
      ],
      description: ['', [Validators.maxLength(2000)]],
    });
  }

  async send() {
    const course: CourseModel = {
      title: this.courseForm.get('title')!.value,
      description: this.courseForm.get('description')!.value,
      creatorId: this.userStore.getUserId()!,
      lessons: [],
    };

    this.CourseManagerService.addCourse(course).subscribe({
      next: () => {
        this.toastr.success('Course has been successfully added!');
        this.router.navigate(['courses', 'instructor']);
      },
      error: () =>
        this.toastr.error('Failed to add the course. Please try again.'),
    });
  }
}
