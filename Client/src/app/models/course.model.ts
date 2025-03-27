import { LessonModel } from './lesson.model';

export type CourseModel = {
  id?: string;
  creatorId: string;
  title: string;
  description: string;
  createdAt?: Date;
  lessons?: LessonModel[];
};
