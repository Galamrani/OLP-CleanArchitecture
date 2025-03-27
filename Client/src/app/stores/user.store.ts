import { Injectable, Signal, signal, effect, computed } from '@angular/core';
import { UserModel } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class UserStore {
  private _user = signal<UserModel | null>(this.loadUserFromLocalStorage()); // Load from local storage

  isLoggedIn = computed(() => this._user() !== null);
  getUserName = computed(() => this._user()?.name);
  getUserId = computed(() => this._user()?.id);

  constructor() {
    // Automatically sync user state to local storage when updated
    effect(() => {
      this.saveUserToLocalStorage(this._user());
    });
  }

  get user(): Signal<UserModel | null> {
    return this._user;
  }

  setUser(newUser: UserModel) {
    this._user.set(newUser);
  }

  clearUser() {
    this._user.set(null);
    localStorage.removeItem('user');
  }

  private loadUserFromLocalStorage(): UserModel | null {
    const userData = localStorage.getItem('user');
    return userData ? JSON.parse(userData) : null;
  }

  private saveUserToLocalStorage(user: UserModel | null): void {
    if (user) {
      localStorage.setItem('user', JSON.stringify(user));
    } else {
      localStorage.removeItem('user');
    }
  }
}
