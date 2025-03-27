import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CourseManagerService } from '../services/course-manager.service';
import { CourseModel } from '../models/course.model';

/**
 *
 * Preloads the course catalog before navigating to the route.
 * The resolver **does not send data directly to the component**.
 * Instead, it triggers `CourseManagerService.loadCourses()`, which updates
 * the `courses$` BehaviorSubject in the facade.
 *
 * The component **does not receive the resolved data directly** but instead
 * subscribes to `courses$` in `CourseManagerService` (facade) to get the updated course list.
 *
 */

export const courseCatalogResolver: ResolveFn<CourseModel[]> = (
  route,
  state
) => {
  const courseManagerService = inject(CourseManagerService);

  return courseManagerService.loadCourses();
};
