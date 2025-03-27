import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FooterComponent } from '../footer/footer.component';
import { RouterOutlet } from '@angular/router';
import { ViewStore } from '../../../stores/view.store';
import { CourseViewType } from '../../../models/user-view.enum';

@Component({
  selector: 'app-layout',
  imports: [NavbarComponent, FooterComponent, RouterOutlet],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LayoutComponent implements OnInit {
  viewStore = inject(ViewStore);

  ngOnInit() {
    if (!this.viewStore.view()) {
      this.viewStore.setView(CourseViewType.Default);
    }
  }
}
