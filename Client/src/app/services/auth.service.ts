import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/user.model';
import { environment } from '../../environments/environment';
import { map, Observable, tap } from 'rxjs';
import { CredentialsModel } from '../models/credentials.model';
import { jwtDecode } from 'jwt-decode';
import { UserStore } from '../stores/user.store';
import { ViewStore } from '../stores/view.store';
import { CourseStore } from '../stores/course.store';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private userStore: UserStore, private courseStore: CourseStore, private viewStore: ViewStore) { }

  public register(user: UserModel): Observable<true> {
    return this.http.post(environment.api.auth.register, user, { responseType: 'text', })
      .pipe(tap((token) => this.handleAuthToken(token)), map(() => true));
  }

  public login(credentials: CredentialsModel): Observable<true> {
    return this.http.post(environment.api.auth.login, credentials, { responseType: 'text', })
      .pipe(tap((token) => this.handleAuthToken(token)), map(() => true));
  }

  public logout(): void {
    this.userStore.clearUser();
    this.viewStore.clearView();
    this.courseStore.reset();
    sessionStorage.clear();
  }

  private handleAuthToken(token: string) {
    const payload = jwtDecode<{ nameid: string; email: string; unique_name: string; }>(token);

    const userData: UserModel = {
      id: payload.nameid,
      name: payload.unique_name,
      email: payload.email,
      password: undefined,
    };

    sessionStorage.setItem('token', token);
    this.userStore.setUser(userData);
  }
}
