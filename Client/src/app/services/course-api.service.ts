import { inject, Injectable } from '@angular/core';
import { CourseModel } from '../models/course.model';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseApiService {
  private http = inject(HttpClient);

  getAllCourses(): Observable<CourseModel[]> {
    console.log('getAllCourses invoked');
    return this.http.get<CourseModel[]>(environment.baseCoursesUrl);
  }

  getUserEnrolledCourses(): Observable<CourseModel[]> {
    console.log('getUserEnrolledCourses invoked');
    return this.http.get<CourseModel[]>(environment.userEnrolledCoursesUrl);
  }

  getUserCreatedCourses(): Observable<CourseModel[]> {
    console.log('getUserCreatedCourses invoked');
    return this.http.get<CourseModel[]>(environment.userCreatedCoursesUrl);
  }

  getBasicCourseDetails(id: string): Observable<CourseModel> {
    return this.http.get<CourseModel>(
      `${environment.courseBasicDetailsUrl}${id}`
    );
  }

  getFullCourseDetails(id: string): Observable<CourseModel> {
    return this.http.get<CourseModel>(
      `${environment.courseFullDetailsUrl}${id}`
    );
  }

  deleteCourse(id: string): Observable<boolean> {
    return this.http
      .delete(`${environment.baseCoursesUrl}${id}`, { responseType: 'text' })
      .pipe(
        map(() => true),
        catchError(() => of(false))
      );
  }

  addCourse(course: CourseModel): Observable<CourseModel> {
    return this.http.post<CourseModel>(environment.baseCoursesUrl, course);
  }

  updateCourse(course: CourseModel): Observable<CourseModel> {
    return this.http.patch<CourseModel>(
      `${environment.baseCoursesUrl}${course.id}`,
      course
    );
  }

  enrollCourse(courseId: string): Observable<CourseModel> {
    return this.http.post<CourseModel>(
      `${environment.enrollCourseUrl}${courseId}`,
      {}
    );
  }

  unEnrollCourse(courseId: string): Observable<void> {
    return this.http.delete<void>(
      `${environment.unenrollCourseUrl}${courseId}`,
      {}
    );
  }
}
