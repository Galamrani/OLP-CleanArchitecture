import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [MenuComponent, RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavbarComponent {}
