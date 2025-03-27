import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  selector: 'app-progress-bar',
  imports: [CommonModule],
  templateUrl: './progress-bar.component.html',
  styleUrl: './progress-bar.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProgressBarComponent {
  courseCompletionPercentage = input.required<number>();
}
