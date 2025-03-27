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
import { ActivatedRoute, Router } from '@angular/router';
import { CourseModel } from '../../../models/course.model';
import { UserStore } from '../../../stores/user.store';
import { CourseManagerService } from '../../../services/course-manager.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-course',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-course.component.html',
  styleUrl: './edit-course.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditCourseComponent {
  courseForm!: FormGroup;
  course!: CourseModel;

  userStore = inject(UserStore);
  toastr = inject(ToastrService);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private courseManagerService: CourseManagerService
  ) {
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

    const courseId = this.route.snapshot.paramMap.get('id'); // Get from paramMap (not queryParamMap)
    if (!courseId) {
      console.error('Missing courseId in route parameters.');
      return;
    }

    const course = this.courseManagerService.getCreatedCourseById(courseId);
    if (!course) {
      console.error('Course not found.');
      return;
    }

    this.courseForm.patchValue({
      title: course.title,
      description: course.description,
    });
  }

  async send() {
    const course: CourseModel = {
      id: this.route.snapshot.paramMap.get('id')!,
      title: this.courseForm.get('title')!.value,
      description: this.courseForm.get('description')!.value,
      creatorId: this.userStore.getUserId()!,
    };

    this.courseManagerService.updateCourse(course).subscribe({
      next: () => {
        this.toastr.success('Course has been successfully updated!');
        this.router.navigate(['/courses', 'instructor']);
      },
      error: () =>
        this.toastr.error('Unable to update the course. Please try again.'),
    });
  }
}
