import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LessonModel } from '../models/lesson.model';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ProgressModel } from '../models/progress.model';
import { CourseStore } from '../stores/course.store';

// TODO remove this class

@Injectable({
  providedIn: 'root',
})
export class LessonService {
  constructor(private http: HttpClient, private courseStore: CourseStore) { }

  addLesson(courseId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.http.post<LessonModel>(environment.api.courses.lessons.base(courseId), lesson)
      .pipe(tap((newLesson) => this.courseStore.addLesson(courseId, newLesson)));
  }

  deleteLesson(courseId: string, lessonId: string): Observable<boolean> {
    return this.http.delete<boolean>(environment.api.courses.lessons.lesson(courseId, lessonId))
      .pipe(tap((_) => this.courseStore.removeLesson(courseId, lessonId)));
  }

  updateLesson(courseId: string, lessonId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.http.patch<LessonModel>(environment.api.courses.lessons.lesson(courseId, lessonId), lesson)
      .pipe(tap((updatedLesson) => this.courseStore.updateLesson(courseId, lessonId, updatedLesson)));
  }

  addProgress(courseId: string, lessonId: string, progress: ProgressModel): Observable<ProgressModel> {
    return this.http.post<ProgressModel>(environment.api.courses.lessons.progress(courseId, lessonId), progress)
      .pipe(tap((newProgress) => this.courseStore.addLessonProgress(courseId, lessonId, progress)));
  }
}
