import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LessonModel } from '../../../models/lesson.model';
import { CourseManagerService } from '../../../services/course-manager.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-lesson',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add-lesson.component.html',
  styleUrl: './add-lesson.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddLessonComponent implements OnInit {
  lessonForm!: FormGroup;
  courseId?: string | null;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private CourseManagerService: CourseManagerService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.courseId = this.route.snapshot.queryParamMap.get('courseId');
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
  }

  send() {
    if (!this.courseId) {
      this.toastr.error('Invalid course ID! Cannot add lesson.');
      return;
    }

    const lesson: LessonModel = {
      title: this.lessonForm.get('title')!.value,
      description: this.lessonForm.get('description')!.value,
      videoUrl: this.lessonForm.get('videoUrl')!.value,
      courseId: this.courseId,
      progresses: [],
    };

    this.CourseManagerService.addLesson(lesson.courseId, lesson).subscribe({
      next: () => {
        this.toastr.success('Lesson has been successfully added!');
        this.router.navigate(['courses', 'details', this.courseId]);
      },
      error: (error) =>
        this.toastr.error('Failed to add the lesson. Please try again.'),
    });
  }
}
