import { ChangeDetectionStrategy, Component, input, OnInit, output, signal, } from '@angular/core';
import { CourseModel } from '../../../models/course.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CourseViewType } from '../../../models/user-view.enum';
import { ViewStore } from '../../../stores/view.store';
import { UserStore } from '../../../stores/user.store';
import { CourseCatalogService } from '../../../services/course-catalog.service';

@Component({
  selector: 'app-course-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './course-card.component.html',
  styleUrl: './course-card.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseCardComponent implements OnInit {

  course = input.required<CourseModel>();
  deleteCourseClicked = output<string>();
  editCourseClicked = output<CourseModel>();
  unenrollCourseClicked = output<string>();
  enrollCourseClicked = output<string>();
  isEnrolled = signal(false);

  constructor(
    private courseCatalogService: CourseCatalogService,
    private router: Router,
    public viewStore: ViewStore,
    public userStore: UserStore
  ) { }

  ngOnInit(): void {
    this.updateEnrollmentStatus();
  }

  handleDeleteClick() {
    this.deleteCourseClicked.emit(this.course().id!);
  }

  handleEditClick() {
    this.editCourseClicked.emit(this.course());
  }

  handleUnenroll() {
    this.isEnrolled.set(false);
    this.unenrollCourseClicked.emit(this.course().id!);
  }

  handleEnroll() {
    this.isEnrolled.set(true);
    this.enrollCourseClicked.emit(this.course().id!);
  }

  navigateToCourseDetails(courseId: string) {
    this.router.navigate(['/courses', 'details', courseId]);
  }

  private updateEnrollmentStatus() {
    this.isEnrolled.set(
      this.courseCatalogService.isEnrolledToCourse(this.course().id!)
    );
  }
}
