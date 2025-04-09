import { ChangeDetectionStrategy, Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, } from "@angular/forms";
import { LessonModel } from "../../../models/lesson.model";
import { CommonModule } from "@angular/common";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-add-lesson",
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: "./add-lesson.component.html",
  styleUrl: "./add-lesson.component.css",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddLessonComponent implements OnInit {
  lessonForm!: FormGroup;
  @Input() courseId!: string;

  constructor(
    private formBuilder: FormBuilder,
    public activeModal: NgbActiveModal,
  ) { }

  ngOnInit() {
    this.lessonForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      description: ["", [Validators.maxLength(2000)]],
      videoUrl: ["", [Validators.required, Validators.pattern("https?://.+")]],
    });
  }

  send() {
    const lesson: LessonModel = {
      title: this.lessonForm.get("title")!.value,
      description: this.lessonForm.get("description")!.value,
      videoUrl: this.lessonForm.get("videoUrl")!.value,
      courseId: this.courseId,
      progresses: [],
    };
    this.activeModal.close(lesson);
  }
}
