import { ChangeDetectionStrategy, Component, computed, inject, input, output } from '@angular/core';
import { LessonModel } from '../../../models/lesson.model';
import { ViewStore } from '../../../stores/view.store';
import { UserStore } from '../../../stores/user.store';
import { RouterModule } from '@angular/router';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { CommonModule } from '@angular/common';

enum PlayerState {
  UNSTARTED = -1,
  ENDED = 0,
  PLAYING = 1,
  PAUSED = 2,
  BUFFERING = 3,
  CUED = 5
}

@Component({
  selector: 'app-lesson-card',
  imports: [RouterModule, CommonModule, YouTubePlayerModule],
  templateUrl: './lesson-card.component.html',
  styleUrl: './lesson-card.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LessonCardComponent {
  lesson = input.required<LessonModel>();
  deleteLessonClicked = output<{ courseId: string; lessonId: string }>();
  editLessonClicked = output<LessonModel>();
  videoClicked = output<{ courseId: string; lessonId: string; }>();

  isViewed = computed(() => {
    const progresses = this.lesson().progresses;
    return progresses && progresses.length > 0;
  });

  constructor(
    public viewStore: ViewStore,
    public userStore: UserStore,
  ) { }

  handleDeleteClick() {
    this.deleteLessonClicked.emit({
      courseId: this.lesson().courseId,
      lessonId: this.lesson().id!,
    });
  }

  handleEditClick() {
    this.editLessonClicked.emit(this.lesson());
  }

  handleVideoClick() {
    if (!this.viewStore.isStudentView()) return;

    this.videoClicked.emit({
      courseId: this.lesson().courseId,
      lessonId: this.lesson().id!,
    });
  }

  onPlayerStateChange(event: any) {
    if (event.data === PlayerState.PLAYING) {
      this.handleVideoClick();
    }
  }

  getYoutubeVideoId(url: string): string | null {
    const match = url.match(/(?:youtube\.com\/(?:.*v=|.*\/)|youtu\.be\/)([a-zA-Z0-9_-]{11})/);
    return match ? match[1] : null;
  }
}
