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
export class ServerErrorComponent {}
