import { Routes } from '@angular/router';
import { LoginComponent } from './components/forms-area/login/login.component';
import { RegisterComponent } from './components/forms-area/register/register.component';
import { HomePageComponent } from './components/page-area/home-page/home-page.component';
import { courseCatalogResolver } from './resolvers/course-catalog.resolver';
import { courseDetailsResolver } from './resolvers/course-details.resolver';
import { NotFoundComponent } from './components/page-area/not-found/not-found.component';
import { ServerErrorComponent } from './components/page-area/server-error/server-error.component';
import { authGuard } from './guards/auth.guard';
import { viewGuard } from './guards/view.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomePageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  {
    path: 'courses/details/:id',
    loadComponent: () =>
      import(
        './components/course-area/course-details/course-details.component'
      ).then((m) => m.CourseDetailsComponent),
    resolve: { course: courseDetailsResolver },
  },
  {
    path: 'courses/default',
    loadComponent: () =>
      import(
        './components/course-area/course-catalog/course-catalog.component'
      ).then((m) => m.CourseCatalogComponent),
    resolve: { course: courseCatalogResolver },
  },
  {
    path: 'courses/instructor',
    loadComponent: () =>
      import(
        './components/course-area/course-catalog/course-catalog.component'
      ).then((m) => m.CourseCatalogComponent),
    canActivate: [authGuard, viewGuard],
    resolve: { course: courseCatalogResolver },
  },
  {
    path: 'courses/student',
    loadComponent: () =>
      import(
        './components/course-area/course-catalog/course-catalog.component'
      ).then((m) => m.CourseCatalogComponent),
    canActivate: [authGuard, viewGuard],
    resolve: { course: courseCatalogResolver },
  },
  {
    path: 'not-found',
    loadComponent: () =>
      import('./components/page-area/not-found/not-found.component').then(
        (m) => m.NotFoundComponent
      ),
  },
  {
    path: 'server-error',
    loadComponent: () =>
      import('./components/page-area/server-error/server-error.component').then(
        (m) => m.ServerErrorComponent
      ),
  },
  {
    path: '**',
    loadComponent: () =>
      import('./components/page-area/not-found/not-found.component').then(
        (m) => m.NotFoundComponent
      ),
  },
];
