import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { UserStore } from '../../../stores/user.store';
import { Router, RouterLink } from '@angular/router';
import { CourseViewType } from '../../../models/user-view.enum';
import { UserService } from '../../../services/user.service';
import { ViewStore } from '../../../stores/view.store';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-menu',
  imports: [NgbDropdownModule, RouterLink, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MenuComponent {
  CourseViewType = CourseViewType;

  userStore = inject(UserStore);
  viewStore = inject(ViewStore);
  userService = inject(UserService);
  router = inject(Router);
  toastr = inject(ToastrService);

  navigateToCourseCatalog(viewType: CourseViewType) {
    this.viewStore.setView(viewType);
    this.router.navigate(['/courses', viewType]);
  }

  logout() {
    this.userService.logout();
    this.toastr.success('You have successfully logged out.', 'Goodbye!');
    this.router.navigate(['/home']);
  }
}
