import { Routes } from '@angular/router';
import { HomePageComponent } from './components/page-area/home-page/home-page.component';
import { NotFoundComponent } from './components/page-area/not-found/not-found.component';
import { LoginComponent } from './components/forms-area/login/login.component';
import { RegisterComponent } from './components/forms-area/register/register.component';
import { authGuard } from './guards/auth.guard';
import { viewGuard } from './guards/view.guard';
import { ServerErrorComponent } from './components/page-area/server-error/server-error.component';
import { courseDetailsResolver } from './resolvers/course-details.resolver';
import { courseCatalogResolver } from './resolvers/course-catalog.resolver';

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
    path: 'courses/add',
    loadComponent: () =>
      import('./components/forms-area/add-course/add-course.component').then(
        (m) => m.AddCourseComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'lessons/add',
    loadComponent: () =>
      import('./components/forms-area/add-lesson/add-lesson.component').then(
        (m) => m.AddLessonComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'courses/edit/:id',
    loadComponent: () =>
      import('./components/forms-area/edit-course/edit-course.component').then(
        (m) => m.EditCourseComponent
      ),
    canActivate: [authGuard],
  },
  {
    path: 'lessons/edit/:id',
    loadComponent: () =>
      import('./components/forms-area/edit-lesson/edit-lesson.component').then(
        (m) => m.EditLessonComponent
      ),
    canActivate: [authGuard],
  },

  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent },
];
