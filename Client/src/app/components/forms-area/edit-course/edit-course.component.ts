import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CourseModel } from '../../../models/course.model';
import { UserStore } from '../../../stores/user.store';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: "app-edit-course",
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: "./edit-course.component.html",
  styleUrl: "./edit-course.component.css",
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditCourseComponent implements OnInit {
  courseForm!: FormGroup;
  @Input() course!: CourseModel;

  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private userStore: UserStore) { }

  ngOnInit() {
    this.courseForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      description: ["", [Validators.maxLength(2000)]],
    });

    this.courseForm.patchValue({
      title: this.course.title,
      description: this.course.description,
    });
  }

  async send() {
    const course: CourseModel = {
      id: this.course.id,
      title: this.courseForm.get("title")!.value,
      description: this.courseForm.get("description")!.value,
      creatorId: this.userStore.getUserId()!,
    };
    this.activeModal.close(course);
  }
}