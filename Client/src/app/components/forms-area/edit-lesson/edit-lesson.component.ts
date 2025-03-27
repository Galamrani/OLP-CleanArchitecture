import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LessonModel } from '../../../models/lesson.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseManagerService } from '../../../services/course-manager.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-lesson',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-lesson.component.html',
  styleUrl: './edit-lesson.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditLessonComponent {
  lessonForm!: FormGroup;
  lesson!: LessonModel;

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private courseManagerService: CourseManagerService
  ) {
    this.lessonForm = this.formBuilder.group({
      title: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(200),
        ],
      ],
      description: ['', [Validators.maxLength(2000)]],
      videoUrl: ['', [Validators.required, Validators.pattern('https?://.+')]],
    });

    const lessonId = this.route.snapshot.paramMap.get('id'); // Route param
    const courseId = this.route.snapshot.queryParamMap.get('courseId'); // Query param

    if (!lessonId || !courseId) {
      console.error('Missing lessonId or courseId in route parameters.');
      return;
    }

    const course = this.courseManagerService.getCreatedCourseById(courseId);
    const lesson = course?.lessons?.find((l) => l.id === lessonId);

    if (!lesson) {
      console.error('Lesson not found.');
      return;
    }

    this.lessonForm.patchValue({
      title: lesson.title,
      description: lesson.description,
      videoUrl: lesson.videoUrl,
    });
  }

  async send() {
    const id = this.route.snapshot.paramMap.get('id');
    const courseId = this.route.snapshot.queryParamMap.get('courseId');

    const lesson: LessonModel = {
      id: id!,
      title: this.lessonForm.get('title')!.value,
      description: this.lessonForm.get('description')!.value,
      videoUrl: this.lessonForm.get('videoUrl')!.value,
      courseId: courseId!,
    };

    this.courseManagerService.updateLesson(lesson.courseId, lesson).subscribe({
      next: () => {
        this.toastr.success('Lesson has been successfully updated!');
        this.router.navigate(['/courses', 'details', courseId]);
      },
      error: () =>
        this.toastr.error('Unable to update the lesson. Please try again.'),
    });
  }
}
