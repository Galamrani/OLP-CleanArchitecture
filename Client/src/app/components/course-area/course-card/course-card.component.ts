import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  input,
  OnInit,
  output,
  signal,
} from '@angular/core';
import { CourseModel } from '../../../models/course.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CourseViewType } from '../../../models/user-view.enum';
import { ViewStore } from '../../../stores/view.store';
import { CourseManagerService } from '../../../services/course-manager.service';
import { UserStore } from '../../../stores/user.store';

@Component({
  selector: 'app-course-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './course-card.component.html',
  styleUrl: './course-card.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseCardComponent implements OnInit {
  CourseManagerService = inject(CourseManagerService);
  viewStore = inject(ViewStore);
  userStore = inject(UserStore);

  course = input.required<CourseModel>();
  deleteCourseClicked = output<string>();
  unenrollCourseClicked = output<string>();
  enrollCourseClicked = output<string>();
  CourseViewType = CourseViewType;

  isEnrolled = signal(false);

  ngOnInit(): void {
    this.updateEnrollmentStatus();
  }

  handleDeleteClick() {
    this.deleteCourseClicked.emit(this.course().id!);
  }

  handleUnenroll() {
    this.isEnrolled.set(false);
    this.unenrollCourseClicked.emit(this.course().id!);
  }

  handleEnroll() {
    this.isEnrolled.set(true);
    this.enrollCourseClicked.emit(this.course().id!);
  }

  private updateEnrollmentStatus() {
    this.isEnrolled.set(
      this.CourseManagerService.isEnrolledToCourse(this.course().id!)
    );
  }
}
