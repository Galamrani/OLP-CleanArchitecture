import { Injectable } from '@angular/core';
import { CourseModel } from '../models/course.model';
import { LessonModel } from '../models/lesson.model';
import { ProgressModel } from '../models/progress.model';

/**
 *
 * Manages **created and enrolled user courses**, caching data in memory and LocalStorage
 * to reduce API calls and ensure offline availability.
 * This store is **only accessed and modified by the CourseManagerService (Facade)**
 * to maintain consistency.
 *
 * **Key Features:**
 * - Stores courses in **Map structures** for quick access.
 * - **Syncs with LocalStorage** to persist data across sessions.
 * - Handles **CRUD operations** for courses, lessons, and progress.
 * - Provides **efficient getters** for accessing cached courses.
 *
 * **Why Use It?**
 * - Reduces API requests by caching data.
 * - Ensures **offline support** with LocalStorage.
 * - Maintains **state consistency** with the API through the facade.
 */

enum CoursesStorageKey {
  CreatedCourses = 'createdCourses',
  EnrolledCourses = 'enrolledCourses',
}

@Injectable({ providedIn: 'root' })
export class CourseStore {
  private _createdCourses: Map<string, CourseModel> =
    this.loadFromStorage(CoursesStorageKey.CreatedCourses) || new Map();
  private _enrolledCourses: Map<string, CourseModel> =
    this.loadFromStorage(CoursesStorageKey.EnrolledCourses) || new Map();

  reset() {
    this._createdCourses.clear();
    this._enrolledCourses.clear();

    localStorage.removeItem(CoursesStorageKey.CreatedCourses);
    localStorage.removeItem(CoursesStorageKey.EnrolledCourses);
  }

  getCreatedCoursesArray(): CourseModel[] {
    return Array.from(this._createdCourses.values());
  }

  getEnrolledCoursesArray(): CourseModel[] {
    return Array.from(this._enrolledCourses.values());
  }

  getCreatedCourseById(courseId: string): CourseModel | undefined {
    return this._createdCourses.get(courseId);
  }

  isCreatedCoursesEmpty(): boolean {
    return this._createdCourses.size === 0;
  }

  isEnrolledCoursesEmpty(): boolean {
    return this._enrolledCourses.size === 0;
  }

  isCreatedCourseExists(courseId: string): boolean {
    return this._createdCourses.has(courseId);
  }

  isEnrolledToCourse(courseId: string): boolean {
    return this._enrolledCourses.has(courseId);
  }

  setCreatedCourses(courses: CourseModel[]) {
    this._createdCourses = new Map(
      courses.map((course) => [course.id!, course])
    );
    this.saveToStorage(CoursesStorageKey.CreatedCourses, this._createdCourses);
  }

  setEnrolledCourses(courses: CourseModel[]) {
    this._enrolledCourses = new Map(
      courses.map((course) => [course.id!, course])
    );
    this.saveToStorage(
      CoursesStorageKey.EnrolledCourses,
      this._enrolledCourses
    );
  }

  updateEnrolledCourse(course: CourseModel) {
    if (!course.id) return;

    if (this._enrolledCourses.has(course.id)) {
      this._enrolledCourses.set(course.id, course);
      this.saveToStorage(
        CoursesStorageKey.EnrolledCourses,
        this._enrolledCourses
      );
    }
  }

  pushCreatedCourse(course: CourseModel) {
    if (!course.id) return;
    this._createdCourses.set(course.id, course);
    this.saveToStorage(CoursesStorageKey.CreatedCourses, this._createdCourses);
  }

  pushEnrolledCourse(course: CourseModel) {
    if (!course.id) return;
    this._enrolledCourses.set(course.id, course);
    this.saveToStorage(
      CoursesStorageKey.EnrolledCourses,
      this._enrolledCourses
    );
  }

  removeCourse(courseId: string) {
    this.removeEnrolledCourse(courseId);
    this._createdCourses.delete(courseId);
    this._enrolledCourses.delete(courseId);
    this.saveToStorage(CoursesStorageKey.CreatedCourses, this._createdCourses);
  }

  removeEnrolledCourse(courseId: string) {
    this._enrolledCourses.delete(courseId);
    this.saveToStorage(
      CoursesStorageKey.EnrolledCourses,
      this._enrolledCourses
    );
  }

  pushLesson(courseId: string, lesson: LessonModel) {
    const course = this._createdCourses.get(courseId);
    console.log(course); // to delete
    if (!course || !course.lessons) {
      return;
    }

    const lessonIndex = course.lessons.findIndex((l) => l.id === lesson.id);

    // Update existing lesson
    if (lessonIndex !== -1) course.lessons[lessonIndex] = lesson;
    // Add new lesson
    else course.lessons.push(lesson);

    this.saveUpdatedCourse(course, CoursesStorageKey.CreatedCourses);
  }

  removeLesson(courseId: string, lessonId: string) {
    const course = this._createdCourses.get(courseId);
    if (!course || !course.lessons) return;

    course.lessons = course.lessons.filter((lesson) => lesson.id !== lessonId);
    this.saveUpdatedCourse(course, CoursesStorageKey.CreatedCourses);
  }

  pushLessonProgress(
    courseId: string,
    lessonId: string,
    progress: ProgressModel
  ) {
    const course = this._enrolledCourses.get(courseId);
    if (!course || !course.lessons) return;

    const lesson = course.lessons.find((l) => l.id === lessonId);
    if (!lesson || !lesson.progresses) return;

    lesson.progresses.push(progress);

    this.saveUpdatedCourse(course, CoursesStorageKey.EnrolledCourses);
  }

  private saveUpdatedCourse(
    course: CourseModel,
    courseStorageKey: CoursesStorageKey
  ) {
    if (courseStorageKey === CoursesStorageKey.CreatedCourses) {
      this._createdCourses.set(course.id!, course);
      this.saveToStorage(courseStorageKey, this._createdCourses);
    } else if (courseStorageKey === CoursesStorageKey.EnrolledCourses) {
      this._enrolledCourses.set(course.id!, course);
      this.saveToStorage(courseStorageKey, this._enrolledCourses);
    }
  }

  private loadFromStorage(key: string): Map<string, CourseModel> {
    const data = localStorage.getItem(key);
    try {
      const parsedData = data ? JSON.parse(data) : [];
      return new Map(parsedData);
    } catch (error) {
      console.error(`Error loading ${key} from storage:`, error);
      return new Map();
    }
  }

  private saveToStorage(key: string, data: Map<string, CourseModel>) {
    // Convert Map to array before storing, because Map is not directly serializable in JavaScript!
    localStorage.setItem(key, JSON.stringify(Array.from(data.entries())));
  }
}
