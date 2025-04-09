import { ChangeDetectionStrategy, Component, OnInit, } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, } from "@angular/forms";
import { CourseModel } from "../../../models/course.model";
import { UserStore } from "../../../stores/user.store";
import { CommonModule } from "@angular/common";
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: "app-add-course",
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: "./add-course.component.html",
  styleUrl: "./add-course.component.css",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddCourseComponent implements OnInit {
  courseForm!: FormGroup;

  constructor(
    public activeModal: NgbActiveModal,
    private userStore: UserStore,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.courseForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      description: ["", [Validators.maxLength(2000)]],
    });
  }

  async send() {
    const course: CourseModel = {
      title: this.courseForm.get("title")!.value,
      description: this.courseForm.get("description")!.value,
      creatorId: this.userStore.getUserId()!,
      lessons: [],
    };
    this.activeModal.close(course);
  }
}
