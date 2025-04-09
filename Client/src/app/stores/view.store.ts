import { computed, effect, Injectable, Signal, signal } from '@angular/core';
import { CourseViewType } from '../models/user-view.enum';

@Injectable({
  providedIn: 'root',
})
export class ViewStore {
  private _view = signal<CourseViewType>(this.loadViewFromSessionStorage());

  isInstructorView = computed(() => this._view() === CourseViewType.Instructor);
  isStudentView = computed(() => this._view() === CourseViewType.Student);
  isDefaultView = computed(() => this._view() === CourseViewType.Default);

  constructor() {
    // Sync changes to sessionStorage automatically
    effect(() => {
      this.saveViewToSessionStorage(this._view());
    });
  }

  get view(): Signal<CourseViewType> {
    return this._view;
  }

  setView(newView: CourseViewType) {
    this._view.set(newView);
  }

  clearView() {
    this._view.set(CourseViewType.Default);
    sessionStorage.removeItem('userView');
  }

  private loadViewFromSessionStorage(): CourseViewType {
    const storedView = sessionStorage.getItem('userView');
    return (storedView as CourseViewType) || CourseViewType.Default;
  }

  private saveViewToSessionStorage(view: CourseViewType): void {
    sessionStorage.setItem('userView', view);
  }
}
