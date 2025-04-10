import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LessonModel } from '../models/lesson.model';
import { BehaviorSubject, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ProgressModel } from '../models/progress.model';
import { CourseStore } from '../stores/course.store';
import { CourseModel } from '../models/course.model';
import { ViewStore } from '../stores/view.store';
import { CourseType } from '../models/course-type.enum';

@Injectable({
  providedIn: 'root'
})
export class CourseDetailsService {
  private _courseSubject = new BehaviorSubject<CourseModel | null>(null);
  readonly course$ = this._courseSubject.asObservable();

  constructor(private http: HttpClient, private courseStore: CourseStore, private viewStore: ViewStore) { }

  loadCourseStream(courseId: string): Observable<CourseModel> {
    return this.getCourseDetails(courseId).pipe(
      tap((course) => this._courseSubject.next(course))
    );
  }

  getCourseStream(): Observable<CourseModel | null> {
    return this.course$;
  }

  getCourseDetails(courseId: string): Observable<CourseModel> {
    const courseType = this.getCourseTypeForCurrentView();
    const cachedCourse = this.courseStore.getCourseDetails(courseId, courseType);

    if (cachedCourse && cachedCourse.lessons?.length! > 0) {
      this._courseSubject.next(cachedCourse);
      return of(cachedCourse);
    }

    return this.http.get<CourseModel>(environment.api.courses.course(courseId)).pipe(
      tap((course) => {
        this.saveCourseToStore(course, courseType);
        this._courseSubject.next(course);
      })
    );
  }

  addLesson(courseId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.http.post<LessonModel>(environment.api.courses.lessons.base(courseId), lesson)
      .pipe(tap((newLesson) => {
        this.courseStore.addLesson(courseId, newLesson),
          this.emitUpdate(courseId);
      }));
  }

  deleteLesson(courseId: string, lessonId: string): Observable<void> {
    return this.http.delete<void>(environment.api.courses.lessons.lesson(courseId, lessonId))
      .pipe(tap((_) => {
        this.courseStore.removeLesson(courseId, lessonId),
          this.emitUpdate(courseId);
      }));
  }

  updateLesson(courseId: string, lessonId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.http.patch<LessonModel>(environment.api.courses.lessons.lesson(courseId, lessonId), lesson)
      .pipe(tap((updatedLesson) => {
        this.courseStore.updateLesson(courseId, lessonId, updatedLesson),
          this.emitUpdate(courseId);
      }));
  }

  addProgress(courseId: string, lessonId: string, progress: ProgressModel): Observable<ProgressModel> {
    return this.http.post<ProgressModel>(environment.api.courses.lessons.progress(courseId, lessonId), progress)
      .pipe(tap((newProgress) => {
        this.courseStore.addLessonProgress(courseId, lessonId, newProgress),
          this.emitUpdate(courseId);
      }));
  }

  isLessonViewed(courseId: string, lessonId: string): boolean {
    return this.courseStore.isLessonViewed(courseId, lessonId);
  }

  private emitUpdate(courseId: string): void {
    const courseType = this.viewStore.isStudentView() ? CourseType.Enrolled : CourseType.All;
    const course = this.courseStore.getCourseDetails(courseId, courseType)
    if (course) this._courseSubject.next({ ...course });;
  }

  private getCourseTypeForCurrentView(): CourseType {
    if (this.viewStore.isInstructorView()) return CourseType.Created;
    if (this.viewStore.isStudentView()) return CourseType.Enrolled;
    return CourseType.All;
  }

  private saveCourseToStore(course: CourseModel, courseType: CourseType): void {
    if (courseType === CourseType.Created) this.courseStore.addCreatedCourse(course);
    else if (courseType === CourseType.Enrolled) this.courseStore.addEnrolledCourse(course);
  }
}
