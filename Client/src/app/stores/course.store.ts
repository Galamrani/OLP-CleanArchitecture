import { Injectable } from '@angular/core';
import { CourseModel } from '../models/course.model';
import { LessonModel } from '../models/lesson.model';
import { ProgressModel } from '../models/progress.model';
import { CourseType } from '../models/course-type.enum';
import { environment } from '../../environments/environment';


@Injectable({ providedIn: 'root' })
export class CourseStore {

  // Private state
  private _createdCourses = new Map<string, CourseModel>();
  private _enrolledCourses = new Map<string, CourseModel>();

  constructor() {
    // Initialize state from storage
    this._createdCourses = this.loadFromStorage(CourseType.Created);
    this._enrolledCourses = this.loadFromStorage(CourseType.Enrolled);
  }

  // Reset all data
  reset(): void {
    this._createdCourses.clear();
    this._enrolledCourses.clear();
    sessionStorage.removeItem(CourseType.Created);
    sessionStorage.removeItem(CourseType.Enrolled);
  }

  // Course getters
  getCreatedCourses(): CourseModel[] {
    return Array.from(this._createdCourses.values());
  }

  getEnrolledCourses(): CourseModel[] {
    return Array.from(this._enrolledCourses.values());
  }

  getCreatedCourseById(courseId: string): CourseModel | undefined {
    return this._createdCourses.get(courseId);
  }

  getCourseDetails(courseId: string, courseType: CourseType): CourseModel | undefined {
    let course: CourseModel | undefined;

    if (courseType === CourseType.Enrolled) {
      course = this._enrolledCourses.get(courseId);
    } else if (courseType === CourseType.Created) {
      course = this._createdCourses.get(courseId);
    } else if (courseType === CourseType.All) {
      course = this._enrolledCourses.get(courseId) ?? this._createdCourses.get(courseId);
    }

    if (!course) return undefined;

    return {
      ...course,
      lessons: (course.lessons ?? []).map(lesson => ({
        ...lesson,
        progresses: [...(lesson.progresses ?? [])]
      }))
    };
  }


  // Course setters
  setCourseDetails(courseId: string, course: CourseModel): void {
    throw new Error('Method not implemented.');
  }

  setCreatedCourses(courses: CourseModel[]): void {
    this._createdCourses = new Map(
      courses.filter((c) => c.id).map((c) => [c.id!, c])
    );
    this.saveToStorage(CourseType.Created);
  }

  setEnrolledCourses(courses: CourseModel[]): void {
    this._enrolledCourses = new Map(
      courses.filter((c) => c.id).map((c) => [c.id!, c])
    );
    this.saveToStorage(CourseType.Enrolled);
  }

  // Course modifiers
  addCreatedCourse(course: CourseModel): void {
    if (!course.id) return;
    this._createdCourses.set(course.id, course);
    this.saveToStorage(CourseType.Created);
  }

  addEnrolledCourse(course: CourseModel): void {
    if (!course.id) return;
    this._enrolledCourses.set(course.id, course);
    this.saveToStorage(CourseType.Enrolled);
  }

  updateCreatedCourse(course: CourseModel): void {
    if (!course.id) return;

    if (this._createdCourses.has(course.id)) {
      this._createdCourses.set(course.id, course);
      this.saveToStorage(CourseType.Created);
    }

    if (this._enrolledCourses.has(course.id)) {
      this._enrolledCourses.set(course.id, course);
      this.saveToStorage(CourseType.Enrolled);
    }
  }


  removeCourse(courseId: string): void {
    const createdDeleted = this._createdCourses.delete(courseId);
    const enrolledDeleted = this._enrolledCourses.delete(courseId);

    if (createdDeleted) {
      this.saveToStorage(CourseType.Created);
    }

    if (enrolledDeleted) {
      this.saveToStorage(CourseType.Enrolled);
    }
  }

  removeEnrolledCourse(courseId: string): void {
    if (this._enrolledCourses.delete(courseId)) {
      this.saveToStorage(CourseType.Enrolled);
    }
  }

  isEnrolledToCourse(courseId: string): boolean {
    return this._enrolledCourses.has(courseId);
  }

  // NOTE why the code so complected
  // Lesson methods
  addLesson(courseId: string, lesson: LessonModel): void {
    const createdCourse = this._createdCourses.get(courseId);
    if (createdCourse?.lessons) {
      const index = createdCourse.lessons.findIndex(l => l.id === lesson.id);
      if (index !== -1) {
        createdCourse.lessons[index] = lesson;
      } else {
        createdCourse.lessons.push(lesson);
      }
      this.saveToStorage(CourseType.Created);
    }

    const enrolledCourse = this._enrolledCourses.get(courseId);
    if (enrolledCourse?.lessons) {
      const index = enrolledCourse.lessons.findIndex(l => l.id === lesson.id);
      if (index !== -1) {
        // Preserve progress data when updating existing lessons
        const originalLesson = enrolledCourse.lessons[index];
        const updatedLesson = {
          ...lesson,
          progresses: originalLesson.progresses || [] // Keep original progress data if it exists
        };
        enrolledCourse.lessons[index] = updatedLesson;
      } else {
        // Add new lesson with empty progress array
        enrolledCourse.lessons.push({
          ...lesson,
          progresses: [] // Initialize with empty progress array
        });
      }
      this.saveToStorage(CourseType.Enrolled);
    }
  }

  removeLesson(courseId: string, lessonId: string): void {
    const createdCourse = this._createdCourses.get(courseId);
    if (createdCourse?.lessons) {
      createdCourse.lessons = createdCourse.lessons.filter(l => l.id !== lessonId);
      this.saveToStorage(CourseType.Created);
    }

    const enrolledCourse = this._enrolledCourses.get(courseId);
    if (enrolledCourse?.lessons) {
      enrolledCourse.lessons = enrolledCourse.lessons.filter(l => l.id !== lessonId);
      this.saveToStorage(CourseType.Enrolled);
    }
  }

  addLessonProgress(courseId: string, lessonId: string, progress: ProgressModel): void {
    const course = this._enrolledCourses.get(courseId);
    if (!course || !course.lessons) return;

    const lesson = course.lessons.find((l) => l.id === lessonId);
    if (!lesson || !lesson.progresses) return;

    lesson.progresses.push(progress);
    this.saveToStorage(CourseType.Enrolled);
  }

  // NOTE why the code so complected
  updateLesson(courseId: string, lessonId: string, lesson: LessonModel): void {
    // Update in created courses if it exists
    const createdCourse = this._createdCourses.get(courseId);
    if (createdCourse?.lessons) {
      const lessonIndex = createdCourse.lessons.findIndex(l => l.id === lessonId);
      if (lessonIndex !== -1) {
        const updatedLesson = { ...lesson };
        if (!updatedLesson.id) updatedLesson.id = lessonId;

        // Update the lesson
        createdCourse.lessons[lessonIndex] = updatedLesson;
        this.saveToStorage(CourseType.Created);
      }
    }

    // Update in enrolled courses if it exists
    const enrolledCourse = this._enrolledCourses.get(courseId);
    if (enrolledCourse?.lessons) {
      const lessonIndex = enrolledCourse.lessons.findIndex(l => l.id === lessonId);
      if (lessonIndex !== -1) {
        // Create updated lesson while preserving progresses
        const originalLesson = enrolledCourse.lessons[lessonIndex];
        const updatedLesson = {
          ...originalLesson,  // Keep original properties
          ...lesson,    // Apply updates
          progresses: originalLesson.progresses // Preserve progresses
        };

        // Update the lesson
        enrolledCourse.lessons[lessonIndex] = updatedLesson;
        this.saveToStorage(CourseType.Enrolled);
      }
    }
  }

  // Private helper methods
  private loadFromStorage(courseType: CourseType): Map<string, CourseModel> {
    try {
      const raw = sessionStorage.getItem(courseType);
      const parsed = raw ? JSON.parse(raw) : [];
      return new Map(parsed);
    } catch (err) {
      if (!environment.production) {
        console.error(`Failed to load ${courseType} from sessionStorage:`, err);
      }
      return new Map();
    }
  }

  private saveToStorage(courseType: CourseType): void {
    try {
      const data =
        courseType === CourseType.Created
          ? this._createdCourses
          : this._enrolledCourses;

      sessionStorage.setItem(
        courseType,
        JSON.stringify(Array.from(data.entries()))
      );
    } catch (err) {
      if (!environment.production) {
        console.error(`Failed to save ${courseType} to sessionStorage:`, err);
      }
    }
  }
}
