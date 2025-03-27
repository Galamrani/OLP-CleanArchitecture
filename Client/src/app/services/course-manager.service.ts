import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, delay, Observable, of, tap } from "rxjs";
import { CourseModel } from "../models/course.model";
import { CourseStore } from "../stores/course.store";
import { ViewStore } from "../stores/view.store";
import { CourseViewType } from "../models/user-view.enum";
import { LessonModel } from "../models/lesson.model";
import { ProgressModel } from "../models/progress.model";
import { CourseApiService } from "./course-api.service";
import { LessonApiService } from "./lesson-api.service";

/**
 *
 * This service acts as a **facade** for managing courses, ensuring smooth interaction
 * between the **CourseStore (local state)** and the **API services**.
 *
 * **Key Responsibilities:**
 * - **Manages course state**: Keeps `CourseStore` in sync with API data.
 * - **Handles all API interactions** for courses, lessons, and progress.
 * - **Exposes `courses$`**: A reactive `BehaviorSubject` stream used by components.
 * - **Implements caching**: Reduces API calls by utilizing stored data in `CourseStore`.
 * - **Simplifies component logic**: Components interact with a single, centralized API.
 *
 * **How It Works:**
 * - **CRUD operations** modify both API and store (`addCourse`, `updateLesson`, etc.).
 * - **Resolvers & UI load courses through `loadCourses()`**, which:
 *   - Uses `ViewStore` to determine the correct data to load (Instructor vs. Student).
 *   - Checks the store first before fetching from the API.
 * - **Components subscribe to `courses$`** to get real-time updates.
 *
 * **Why Use This Facade?**
 * - Avoids **duplicate API calls** by caching data.
 * - Ensures **state consistency** between API and UI.
 * - Provides a **single source** for courses.
 */

@Injectable({
  providedIn: "root",
})
export class CourseManagerService {
  private courseStore = inject(CourseStore);
  private viewStore = inject(ViewStore);
  private courseApiService = inject(CourseApiService);
  private lessonApiService = inject(LessonApiService);

  private coursesSubject = new BehaviorSubject<CourseModel[]>([]); // Holds the actual list of courses and allows updates
  courses$ = this.coursesSubject.asObservable(); // Exposes the courses as a read-only observable so components can subscribe but cannot modify directly

  reset() {
    this.courseStore.reset();
    this.coursesSubject.next([]);
  }

  isEnrolledToCourse(courseId: string) {
    return this.courseStore.isEnrolledToCourse(courseId);
  }

  getCreatedCourseById(courseId: string): CourseModel | undefined {
    const cachedCourse = this.courseStore.getCreatedCourseById(courseId);
    return cachedCourse;
  }

  deleteCourse(id: string): Observable<boolean> {
    return this.courseApiService.deleteCourse(id).pipe(
      tap(() => {
        this.courseStore.removeCourse(id);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  addCourse(course: CourseModel): Observable<CourseModel> {
    return this.courseApiService.addCourse(course).pipe(
      tap((newCourse) => {
        this.courseStore.pushCreatedCourse(newCourse);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  updateCourse(course: CourseModel): Observable<CourseModel> {
    return this.courseApiService.updateCourse(course).pipe(
      tap((updatedCourse) => {
        this.courseStore.pushCreatedCourse(updatedCourse);
        this.courseStore.updateEnrolledCourse(updatedCourse);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  enrollCourse(courseId: string): Observable<CourseModel> {
    return this.courseApiService.enrollCourse(courseId).pipe(
      tap((enrolledCourse) => {
        this.courseStore.pushEnrolledCourse(enrolledCourse);
      })
    );
  }

  unenrollCourse(courseId: string): Observable<void> {
    return this.courseApiService.unEnrollCourse(courseId).pipe(
      tap(() => {
        this.courseStore.removeEnrolledCourse(courseId);
        this.coursesSubject.next(this.courseStore.getEnrolledCoursesArray());
      })
    );
  }

  addLesson(courseId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.lessonApiService.addLesson(lesson).pipe(
      tap((newLesson) => {
        this.courseStore.pushLesson(courseId, newLesson);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  deleteLesson(courseId: string, lessonId: string): Observable<boolean> {
    return this.lessonApiService.deleteLesson(lessonId).pipe(
      tap(() => {
        this.courseStore.removeLesson(courseId, lessonId);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  updateLesson(courseId: string, lesson: LessonModel): Observable<LessonModel> {
    return this.lessonApiService.updateLesson(lesson).pipe(
      tap((updatedLesson) => {
        this.courseStore.pushLesson(courseId, updatedLesson);
        this.coursesSubject.next(this.courseStore.getCreatedCoursesArray());
      })
    );
  }

  addProgress(
    courseId: string,
    lessonId: string,
    progress: ProgressModel
  ): Observable<ProgressModel> {
    return this.lessonApiService.addProgress(progress).pipe(
      tap((newProgress) => {
        this.courseStore.pushLessonProgress(courseId, lessonId, newProgress);
        this.coursesSubject.next(this.courseStore.getEnrolledCoursesArray());
      })
    );
  }

  loadCourseDetails(courseId: string): Observable<CourseModel> {
    switch (this.viewStore.view()) {
      case CourseViewType.Student:
        return this.courseApiService.getFullCourseDetails(courseId).pipe(
          tap((course) => {
            this.courseStore.updateEnrolledCourse(course);
            this.coursesSubject.next(
              this.courseStore.getEnrolledCoursesArray()
            );
          })
        );

      case CourseViewType.Instructor:
        const cachedCourse = this.courseStore.getCreatedCourseById(courseId);
        if (cachedCourse) return of(cachedCourse); // Wrap the existing course in an Observable
        return this.courseApiService.getFullCourseDetails(courseId);

      default:
        return this.courseApiService.getBasicCourseDetails(courseId);
    }
  }

  loadCourses(): Observable<CourseModel[]> {
    switch (this.viewStore.view()) {
      case CourseViewType.Instructor:
        return this.loadCreatedCourses();
      case CourseViewType.Student:
        return this.loadEnrolledCourses();
      default:
        return this.loadAllCourses();
    }
  }

  private loadAllCourses(): Observable<CourseModel[]> {
    return this.courseApiService.getAllCourses().pipe(
      tap((courses) => {
        this.coursesSubject.next(courses);
      })
    );
  }

  private loadEnrolledCourses(): Observable<CourseModel[]> {
    if (this.courseStore.isEnrolledCoursesEmpty()) {
      return this.courseApiService.getUserEnrolledCourses().pipe(
        tap((courses) => {
          this.coursesSubject.next(courses);
          this.courseStore.setEnrolledCourses(courses);
        })
      );
    }
    const cachedCourses = this.courseStore.getEnrolledCoursesArray();
    this.coursesSubject.next(cachedCourses);
    return of(cachedCourses);
  }

  private loadCreatedCourses(): Observable<CourseModel[]> {
    if (this.courseStore.isCreatedCoursesEmpty()) {
      return this.courseApiService.getUserCreatedCourses().pipe(
        tap((courses) => {
          this.coursesSubject.next(courses);
          this.courseStore.setCreatedCourses(courses);
        })
      );
    }
    const cachedCourses = this.courseStore.getCreatedCoursesArray();
    this.coursesSubject.next(cachedCourses);
    return of(cachedCourses);
  }
}
