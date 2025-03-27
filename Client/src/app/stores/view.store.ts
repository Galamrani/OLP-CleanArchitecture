import { Injectable, Signal, signal } from '@angular/core';
import { CourseViewType } from '../models/user-view.enum';

@Injectable({ providedIn: 'root' })
export class ViewStore {
  private _view = signal<CourseViewType>(this.getStoredView());

  constructor() {
    (localStorage.getItem('userView') as CourseViewType) ||
      CourseViewType.Default;
  }

  get view(): Signal<CourseViewType> {
    return this._view;
  }

  setView(newView: CourseViewType) {
    localStorage.setItem('userView', newView);
    this._view.set(newView);
  }

  clearView() {
    localStorage.removeItem('userView');
  }

  private getStoredView(): CourseViewType {
    const storedView = localStorage.getItem('userView');
    return storedView ? (storedView as CourseViewType) : CourseViewType.Default;
  }
}
