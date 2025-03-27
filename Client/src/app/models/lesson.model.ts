import { ProgressModel } from './progress.model';

export type LessonModel = {
  id?: string;
  courseId: string;
  title: string;
  description: string;
  videoUrl: string;
  progresses?: ProgressModel[];
};
