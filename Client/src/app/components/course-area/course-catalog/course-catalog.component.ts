import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  OnInit,
} from '@angular/core';
import { CourseViewType } from '../../../models/user-view.enum';
import { ViewStore } from '../../../stores/view.store';
import { CourseCardComponent } from '../course-card/course-card.component';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CourseManagerService } from '../../../services/course-manager.service';
import { map, Observable } from 'rxjs';
import { CourseModel } from '../../../models/course.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-course-catalog',
  imports: [CourseCardComponent, RouterModule, CommonModule],
  templateUrl: './course-catalog.component.html',
  styleUrl: './course-catalog.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseCatalogComponent implements OnInit {
  viewStore = inject(ViewStore);
  route = inject(ActivatedRoute);
  toastr = inject(ToastrService);
  CourseManagerService = inject(CourseManagerService);

  courses$: Observable<CourseModel[]> = this.CourseManagerService.courses$;

  CourseViewType = CourseViewType;

  ngOnInit(): void {
    // Load initial data from resolver
    this.route.data.subscribe();
  }

  onDeleteCourse(courseId: string) {
    this.CourseManagerService.deleteCourse(courseId).subscribe({
      next: () => this.toastr.success('Course has been successfully deleted!'),
      error: () =>
        this.toastr.error('Unable to delete the course. Please try again.'),
    });
  }

  onUnenrollCourse(courseId: string) {
    this.CourseManagerService.unenrollCourse(courseId).subscribe({
      next: () =>
        this.toastr.success(
          'You have successfully unenrolled from the course.'
        ),
      error: () => this.toastr.error('Unable to unenroll. Please try again.'),
    });
  }

  onEnrollCourse(courseId: string) {
    this.CourseManagerService.enrollCourse(courseId).subscribe({
      next: () => this.toastr.success('Successfully enrolled in the course!'),
      error: () => this.toastr.error('Enrollment failed. Please try again.'),
    });
  }
}
