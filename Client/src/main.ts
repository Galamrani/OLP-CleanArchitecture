import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { LayoutComponent } from './app/components/layout-area/layout/layout.component';
import { environment } from './environments/environment';

bootstrapApplication(LayoutComponent, appConfig).catch((err) => {
  if (!environment.production) {
    console.error(err)
  }
});
