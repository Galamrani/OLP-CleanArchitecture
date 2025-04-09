import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { UserStore } from '../../../stores/user.store';
import { Router, RouterLink } from '@angular/router';
import { CourseViewType } from '../../../models/user-view.enum';
import { ViewStore } from '../../../stores/view.store';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-menu',
  imports: [NgbDropdownModule, RouterLink, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MenuComponent {
  CourseViewType = CourseViewType;

  constructor(
    public userStore: UserStore,
    private viewStore: ViewStore,
    private authService: AuthService,
    private router: Router,
    private toast: ToastService
  ) { }

  navigateToCourseCatalog(viewType: CourseViewType) {
    this.viewStore.setView(viewType);
    this.router.navigate(['/courses', viewType]);
  }

  navigateToRegisterForm() {
    this.router.navigate(['/register']);
  }
  navigateToLoginForm() {
    this.router.navigate(['/login']);
  }

  logout() {
    this.authService.logout();
    this.toast.showSuccess('You have successfully logged out, Goodbye!');
    this.router.navigate(['/home']);
  }
}
