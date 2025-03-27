import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LessonModel } from '../models/lesson.model';
import { Observable } from 'rxjs';
import { ProgressModel } from '../models/progress.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LessonApiService {
  private http = inject(HttpClient);

  addLesson(lesson: LessonModel): Observable<LessonModel> {
    return this.http.post<LessonModel>(
      `${environment.baseLessonsUrl}${lesson.courseId}`,
      lesson
    );
  }

  addProgress(progress: ProgressModel): Observable<ProgressModel> {
    return this.http.post<ProgressModel>(
      `${environment.addLessonProgressUrl}${progress.lessonId}`,
      progress
    );
  }

  deleteLesson(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${environment.baseLessonsUrl}${id}`);
  }

  updateLesson(lesson: LessonModel): Observable<LessonModel> {
    return this.http.patch<LessonModel>(
      `${environment.baseLessonsUrl}${lesson.id}`,
      lesson
    );
  }
}
