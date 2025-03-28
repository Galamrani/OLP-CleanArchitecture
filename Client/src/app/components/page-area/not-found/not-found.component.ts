import { CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component } from "@angular/core";
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: "app-not-found",
  imports: [RouterLink, CommonModule],
  templateUrl: "./not-found.component.html",
  styleUrl: "./not-found.component.css",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NotFoundComponent {
  errorMessage: string = "An unexpected error occurred.";
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
