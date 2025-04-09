import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CourseModel } from '../models/course.model';
import { CourseDetailsService } from '../services/course-details.service';

export const courseDetailsResolver: ResolveFn<CourseModel> = (route, state) => {
  const courseDetailsService = inject(CourseDetailsService);

  const courseId = route.paramMap.get('id')!;
  return courseDetailsService.loadCourseStream(courseId);
};
