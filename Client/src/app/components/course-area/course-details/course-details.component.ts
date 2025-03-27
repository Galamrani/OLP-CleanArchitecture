import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { CourseModel } from '../../../models/course.model';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { LessonCardComponent } from '../lesson-card/lesson-card.component';
import { ProgressBarComponent } from '../progress-bar/progress-bar.component';
import { ProgressModel } from '../../../models/progress.model';
import { UserStore } from '../../../stores/user.store';
import { CourseManagerService } from '../../../services/course-manager.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-course-details',
  imports: [
    LessonCardComponent,
    ProgressBarComponent,
    RouterModule,
    CommonModule,
  ],
  templateUrl: './course-details.component.html',
  styleUrl: './course-details.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseDetailsComponent implements OnInit {
  courseManagerService = inject(CourseManagerService);
  viewStore = inject(ViewStore);
  userStore = inject(UserStore);
  route = inject(ActivatedRoute);
  toastr = inject(ToastrService);

  CourseViewType = CourseViewType;
  courses$ = this.courseManagerService.courses$;
  currentCourse = signal<CourseModel | null>(null);

  ngOnInit(): void {
    this.route.data.subscribe(({ course }) => {
      this.currentCourse.set(course);
    });
  }

  onDeleteLesson(event: { courseId: string; lessonId: string }) {
    this.courseManagerService
      .deleteLesson(event.courseId, event.lessonId)
      .subscribe({
        next: () =>
          this.toastr.success('Lesson has been successfully deleted!'),
        error: () =>
          this.toastr.error('Failed to delete the lesson. Please try again.'),
      });
  }

  onWatchedVideo(event: {
    isStudent: boolean;
    courseId: string;
    lessonId: string;
  }) {
    if (!event.isStudent) {
      this.toastr.warning(
        'Progress could not be saved. Please enroll in the course first and watch it in student view.'
      );
      return;
    }

    const progress: ProgressModel = {
      userId: this.userStore.getUserId()!,
      lessonId: event.lessonId,
    };
    this.courseManagerService
      .addProgress(event.courseId, event.lessonId, progress)
      .subscribe({
        next: () => this.toastr.success('Progress saved!'),
        error: () => this.toastr.error('Could not save progress. Try again.'),
      });
  }

  calcCourseProgress(course: CourseModel | null): number {
    if (!course?.lessons?.length) return 0;
    const totalLessons = course.lessons.length;
    const completedLessons = course.lessons.filter(
      (lesson) => lesson.progresses && lesson.progresses.length > 0
    ).length;
    return (completedLessons / totalLessons) * 100;
  }
}
