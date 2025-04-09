// course-catalog.resolver.ts
import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CourseModel } from '../models/course.model';
import { CourseCatalogService } from '../services/course-catalog.service';
import { Observable } from 'rxjs';

export const courseCatalogResolver: ResolveFn<CourseModel[] | null> = (): Observable<CourseModel[]> => {
  const courseCatalogService = inject(CourseCatalogService);
  return courseCatalogService.loadCoursesStream();
};
