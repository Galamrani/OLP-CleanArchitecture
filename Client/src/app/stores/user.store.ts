import { computed, effect, Injectable, Signal, signal } from '@angular/core';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserStore {
  private _user = signal<UserModel | null>(this.loadUserFromSessionStorage());

  isLoggedIn = computed(() => this._user() !== null);
  getUserName = computed(() => this._user()?.name);
  getUserId = computed(() => this._user()?.id);

  constructor() {
    // Sync user state to sessionStorage automatically
    effect(() => {
      this.saveUserToSessionStorage(this._user());
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
    sessionStorage.removeItem('user');
  }

  private loadUserFromSessionStorage(): UserModel | null {
    const userData = sessionStorage.getItem('user');
    return userData ? JSON.parse(userData) : null;
  }

  private saveUserToSessionStorage(user: UserModel | null): void {
    if (user) {
      sessionStorage.setItem('user', JSON.stringify(user));
    } else {
      sessionStorage.removeItem('user');
    }
  }
}
