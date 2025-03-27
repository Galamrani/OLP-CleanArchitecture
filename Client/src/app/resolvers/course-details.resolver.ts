import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CourseManagerService } from '../services/course-manager.service';
import { CourseModel } from '../models/course.model';

/**
 *
 * Preloads course details before navigating to the route.
 * Extracts the `id` from the URL and fetches the course data.
 *
 */

export const courseDetailsResolver: ResolveFn<CourseModel> = (route, state) => {
  const courseManagerService = inject(CourseManagerService);

  const courseId = route.paramMap.get('id')!;
  return courseManagerService.loadCourseDetails(courseId);
};
