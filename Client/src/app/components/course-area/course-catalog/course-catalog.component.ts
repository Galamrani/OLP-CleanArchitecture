import { ChangeDetectionStrategy, Component, OnInit, } from '@angular/core';
import { ViewStore } from '../../../stores/view.store';
import { CourseCardComponent } from '../course-card/course-card.component';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { CourseModel } from '../../../models/course.model';
import { CourseCatalogService } from '../../../services/course-catalog.service';
import { UserStore } from '../../../stores/user.store';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCourseComponent } from '../../forms-area/add-course/add-course.component';
import { EditCourseComponent } from '../../forms-area/edit-course/edit-course.component';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-course-catalog',
  imports: [CourseCardComponent, RouterModule, CommonModule],
  templateUrl: './course-catalog.component.html',
  styleUrl: './course-catalog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseCatalogComponent implements OnInit {

  courses$: Observable<CourseModel[]> | undefined;

  constructor(
    public viewStore: ViewStore,
    private userStore: UserStore,
    private route: ActivatedRoute,
    private toast: ToastService,
    private courseCatalogService: CourseCatalogService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    // Trigger resolver side-effect to ensure data is loaded into the course store, (i don't use the resolved value directly)
    this.route.data.subscribe();

    // Connect to the reactive courses stream by view type
    this.courses$ = this.courseCatalogService.getCoursesStream();
  }

  onDeleteCourse(courseId: string) {
    this.courseCatalogService.deleteCourse(courseId).subscribe({
      next: () => this.toast.showSuccess('Course has been successfully deleted!'),
      error: () => this.toast.showError('Unable to delete the course. Please try again.'),
    });
  }

  onUnenrollCourse(courseId: string) {
    const userId = this.userStore.getUserId();
    if (!userId) return;
    this.courseCatalogService.unEnrollCourse(userId, courseId).subscribe({
      next: () => this.toast.showSuccess('You have successfully unenrolled from the course.'),
      error: () => this.toast.showError('Unable to unenroll. Please try again.'),
    });
  }

  onEnrollCourse(courseId: string) {
    const userId = this.userStore.getUserId();
    if (!userId) return;
    this.courseCatalogService.enrollCourse(userId, courseId).subscribe({
      next: () => this.toast.showSuccess('Successfully enrolled in the course!'),
      error: () => this.toast.showError('Enrollment failed. Please try again.'),
    });
  }

  openAddCourseModal() {
    const modalRef = this.modalService.open(AddCourseComponent, { size: 'lg' });
    modalRef.result.then(
      (course: CourseModel) => {
        this.courseCatalogService.addCourse(course).subscribe({
          next: () => this.toast.showSuccess("Course has been successfully added!"),
          error: () => this.toast.showError("Failed to add the course. Please try again.")
        });
      },
      () => { } // Modal dismissed — do nothing
    );
  }

  onEditCourse(course: CourseModel) {
    const modalRef = this.modalService.open(EditCourseComponent, { size: 'lg' });
    modalRef.componentInstance.course = course;
    modalRef.result.then(
      (course: CourseModel) => {
        this.courseCatalogService.updateCourse(course.id!, course).subscribe({
          next: () => this.toast.showSuccess("Course has been successfully updated!"),
          error: () => this.toast.showError("Failed to update the course. Please try again.")
        });
      },
      () => { } // Modal dismissed — do nothing
    );
  }
}
