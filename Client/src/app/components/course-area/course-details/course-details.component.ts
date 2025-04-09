import { CourseDetailsService } from "../../../services/course-details.service";
import { UserStore } from "../../../stores/user.store";
import { ViewStore } from "../../../stores/view.store";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { CourseModel } from "../../../models/course.model";
import { Observable } from "rxjs";
import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";
import { ProgressModel } from "../../../models/progress.model";
import { ProgressBarComponent } from "../progress-bar/progress-bar.component";
import { CommonModule } from "@angular/common";
import { LessonCardComponent } from "../lesson-card/lesson-card.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { AddLessonComponent } from "../../forms-area/add-lesson/add-lesson.component";
import { LessonModel } from "../../../models/lesson.model";
import { EditLessonComponent } from "../../forms-area/edit-lesson/edit-lesson.component";
import { ToastService } from "../../../services/toast.service";


@Component({
  selector: 'app-course-details',
  imports: [LessonCardComponent, ProgressBarComponent, RouterModule, CommonModule,],
  templateUrl: './course-details.component.html',
  styleUrl: './course-details.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseDetailsComponent implements OnInit {

  course$: Observable<CourseModel | null> | undefined;

  constructor(
    private courseDetailsService: CourseDetailsService,
    public viewStore: ViewStore,
    private userStore: UserStore,
    private route: ActivatedRoute,
    private toast: ToastService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.route.data.subscribe();
    this.course$ = this.courseDetailsService.getCourseStream()
  }

  onDeleteLesson(event: { courseId: string; lessonId: string }) {
    this.courseDetailsService.deleteLesson(event.courseId, event.lessonId)
      .subscribe({
        next: () => this.toast.showSuccess('Lesson has been successfully deleted!'),
        error: () => this.toast.showError('Failed to delete the lesson. Please try again.'),
      });
  }

  openAddLessonModal(courseId: string) {
    const modalRef = this.modalService.open(AddLessonComponent, { size: 'lg' });
    modalRef.componentInstance.courseId = courseId;
    modalRef.result.then(
      (lesson: LessonModel) => {
        this.courseDetailsService.addLesson(lesson.courseId, lesson).subscribe({
          next: () => this.toast.showSuccess("Lesson has been successfully added!"),
          error: () => this.toast.showError("Failed to add the lesson. Please try again.")
        });
      },
      () => { } // Modal dismissed — do nothing
    );
  }

  onEditLesson(lesson: LessonModel) {
    const modalRef = this.modalService.open(EditLessonComponent, { size: 'lg' });
    modalRef.componentInstance.lesson = lesson;
    modalRef.result.then(
      (lesson: LessonModel) => {
        this.courseDetailsService.updateLesson(lesson.courseId, lesson.id!, lesson).subscribe({
          next: () => this.toast.showSuccess("Lesson has been successfully updated!"),
          error: () => this.toast.showError("Failed to update the lesson. Please try again.")
        });
      },
      () => { } // Modal dismissed — do nothing
    );
  }

  onWatchedVideo(event: { courseId: string; lessonId: string; }) {

    const progress: ProgressModel = { userId: this.userStore.getUserId()!, lessonId: event.lessonId, };

    this.courseDetailsService.addProgress(event.courseId, event.lessonId, progress).subscribe({
      next: () => this.toast.showSuccess('Progress saved!'),
      error: () => this.toast.showError('Could not save progress. Try again.'),
    });
  }

  calcCourseProgress(course: CourseModel | null): number {
    if (!course?.lessons?.length) return 0;
    const totalLessons = course.lessons.length;
    const completedLessons = course.lessons.filter(
      (lesson) => lesson.progresses && lesson.progresses.length > 0
    ).length;
    return (completedLessons / totalLessons) * 100;
  }
}
