import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserStore } from '../../../stores/user.store';
import { CourseViewType } from '../../../models/user-view.enum';
import { ViewStore } from '../../../stores/view.store';

@Component({
  selector: 'app-home-page',
  imports: [RouterLink],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomePageComponent {
  CourseViewType = CourseViewType;

  userStore = inject(UserStore);
  viewStore = inject(ViewStore);

  ngOnInit(): void {
    this.viewStore.setView(CourseViewType.Default);
  }
}
