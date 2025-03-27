import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-server-error',
  imports: [CommonModule, RouterLink],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ServerErrorComponent {
  errorMessage: string = 'An unexpected error occurred.';
  errorDetails: any = null;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as {
      errorMessage?: string;
      errorDetails?: any;
    };

    if (state) {
      this.errorMessage = state.errorMessage || this.errorMessage;
      this.errorDetails = state.errorDetails || null;
    }
  }
}
