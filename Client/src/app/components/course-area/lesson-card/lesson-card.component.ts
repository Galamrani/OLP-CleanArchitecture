import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
  output,
} from '@angular/core';
import { LessonModel } from '../../../models/lesson.model';
import { VideoService } from '../../../services/video.service';
import { ViewStore } from '../../../stores/view.store';
import { UserStore } from '../../../stores/user.store';
import { CourseViewType } from '../../../models/user-view.enum';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-lesson-card',
  imports: [RouterModule],
  templateUrl: './lesson-card.component.html',
  styleUrl: './lesson-card.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LessonCardComponent {
  lesson = input.required<LessonModel>();
  deleteLessonClicked = output<{ courseId: string; lessonId: string }>();
  videoClicked = output<{
    isStudent: boolean;
    courseId: string;
    lessonId: string;
  }>();
  CourseViewType = CourseViewType;

  videoService = inject(VideoService);
  viewStore = inject(ViewStore);
  userStore = inject(UserStore);

  getYouTubeThumbnail(url: string): string {
    return this.videoService.getYouTubeThumbnail(url);
  }

  handleDeleteClick() {
    this.deleteLessonClicked.emit({
      courseId: this.lesson().courseId,
      lessonId: this.lesson().id!,
    });
  }

  handleVideoClick() {
    this.videoClicked.emit({
      isStudent: this.viewStore.view() === CourseViewType.Student,
      courseId: this.lesson().courseId,
      lessonId: this.lesson().id!,
    });
  }
}
