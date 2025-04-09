import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, of, tap } from 'rxjs';
import { CourseModel } from '../models/course.model';
import { environment } from '../../environments/environment';
import { CourseStore } from '../stores/course.store';
import { ViewStore } from '../stores/view.store';
import { UserStore } from '../stores/user.store';


@Injectable({
  providedIn: 'root',
})
export class CourseCatalogService {

  private _coursesSubject = new BehaviorSubject<CourseModel[]>([]);
  readonly courses$ = this._coursesSubject.asObservable();

  constructor(private http: HttpClient, private courseStore: CourseStore, private viewStore: ViewStore, private userStore: UserStore) { }

  loadCoursesStream(): Observable<CourseModel[]> {
    let course$: Observable<CourseModel[]>;
    const userId = this.userStore.getUserId();

    if (!userId) {
      course$ = this.getCourses();
    } else if (this.viewStore.isInstructorView()) {
      course$ = this.getUserCreatedCourses(userId);
    } else if (this.viewStore.isStudentView()) {
      course$ = this.getUserEnrolledCourses(userId);
    } else {
      course$ = this.getCourses();
    }

    return course$.pipe(
      tap(courses => this._coursesSubject.next(courses))
    );
  }

  getCoursesStream(): Observable<CourseModel[]> {
    return this.courses$;
  }

  // All courses
  getCourses(): Observable<CourseModel[]> {
    return this.http.get<CourseModel[]>(environment.api.courses.base).pipe(
      tap((courses) => this.emitUpdate(courses)))
  }

  // Created courses
  getUserCreatedCourses(userId: string): Observable<CourseModel[]> {
    const cachedCourses = this.courseStore.getCreatedCourses();
    if (cachedCourses.length > 0) return of(cachedCourses);

    return this.http.get<CourseModel[]>(environment.api.users.userCreatedCourses(userId))
      .pipe(tap((courses) => {
        this.courseStore.setCreatedCourses(courses),
          this.emitUpdate();
      }));
  }

  // Enrolled courses
  getUserEnrolledCourses(userId: string): Observable<CourseModel[]> {
    const cachedCourses = this.courseStore.getEnrolledCourses();
    if (cachedCourses.length > 0) return of(cachedCourses);

    return this.http.get<CourseModel[]>(environment.api.users.userEnrolledCourses(userId))
      .pipe(tap((courses) => {
        this.courseStore.setEnrolledCourses(courses),
          this.emitUpdate();
      }));
  }

  deleteCourse(courseId: string): Observable<boolean> {
    return this.http.delete(environment.api.courses.course(courseId), { observe: 'response' })
      .pipe(map((response) => response.status === 204), tap((_) => {
        this.courseStore.removeCourse(courseId),
          this.emitUpdate()
      }));
  }

  addCourse(course: CourseModel): Observable<CourseModel> {
    return this.http.post<CourseModel>(environment.api.courses.base, course)
      .pipe(tap((newCourse) => {
        this.courseStore.addCreatedCourse(newCourse),
          this.emitUpdate();
      }));
  }

  updateCourse(courseId: string, course: CourseModel): Observable<CourseModel> {
    return this.http.patch<CourseModel>(environment.api.courses.course(courseId), course)
      .pipe(tap((updatedCourse) => {
        this.courseStore.updateCreatedCourse(updatedCourse),
          this.emitUpdate();
      }));
  }

  enrollCourse(userId: string, courseId: string): Observable<CourseModel> {
    return this.http.post<CourseModel>(environment.api.users.userEnrolledCourse(userId, courseId), {})
      .pipe(tap((enrolledCourse) => {
        this.courseStore.addEnrolledCourse(enrolledCourse),
          this.emitUpdate();
      }));
  }

  unEnrollCourse(userId: string, courseId: string): Observable<void> {
    return this.http.delete<void>(environment.api.users.userEnrolledCourse(userId, courseId), {})
      .pipe(tap((_) => {
        this.courseStore.removeEnrolledCourse(courseId),
          this.emitUpdate();
      }));
  }

  isEnrolledToCourse(courseId: string): boolean {
    return this.courseStore.isEnrolledToCourse(courseId);
  }

  private emitUpdate(courses?: CourseModel[]): void {
    if (this.viewStore.isInstructorView()) this._coursesSubject.next(this.courseStore.getCreatedCourses());
    else if (this.viewStore.isStudentView()) this._coursesSubject.next(this.courseStore.getEnrolledCourses());
    if (courses) this._coursesSubject.next(courses);
  }
}
