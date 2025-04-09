import { ChangeDetectionStrategy, Component, inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { LessonModel } from "../../../models/lesson.model";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-edit-lesson",
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: "./edit-lesson.component.html",
  styleUrl: "./edit-lesson.component.css",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditLessonComponent implements OnInit {
  lessonForm!: FormGroup;
  lesson!: LessonModel;

  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.lessonForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      description: ["", [Validators.maxLength(2000)]],
      videoUrl: ["", [Validators.required, Validators.pattern("https?://.+")]],
    });

    this.lessonForm.patchValue({
      title: this.lesson.title,
      description: this.lesson.description,
      videoUrl: this.lesson.videoUrl,
    });
  }

  async send() {
    const lesson: LessonModel = {
      id: this.lesson.id,
      title: this.lessonForm.get("title")!.value,
      description: this.lessonForm.get("description")!.value,
      videoUrl: this.lessonForm.get("videoUrl")!.value,
      courseId: this.lesson.courseId,
    };
    this.activeModal.close(lesson);
  }
}
