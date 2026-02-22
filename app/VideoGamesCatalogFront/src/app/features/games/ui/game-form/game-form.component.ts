import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  inject,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { GameModel, GameFormValue } from '../../data-access/game.model';

@Component({
  selector: 'app-game-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <div class="mb-3">
        <label for="title" class="form-label"
          >Title <span class="text-danger">*</span></label
        >
        <input
          type="text"
          id="title"
          class="form-control"
          formControlName="title"
          [class.is-invalid]="isFieldInvalid('title')"
          placeholder="Enter game title"
        />
        @if (isFieldInvalid('title')) {
          <div class="invalid-feedback">
            @if (form.get('title')?.errors?.['required']) {
              Title is required.
            }
            @if (form.get('title')?.errors?.['maxlength']) {
              Title cannot exceed 200 characters.
            }
          </div>
        }
      </div>

      <div class="mb-3">
        <label for="description" class="form-label">Description</label>
        <textarea
          id="description"
          class="form-control"
          formControlName="description"
          [class.is-invalid]="isFieldInvalid('description')"
          rows="4"
          placeholder="Enter game description"
        ></textarea>
        @if (isFieldInvalid('description')) {
          <div class="invalid-feedback">
            @if (form.get('description')?.errors?.['maxlength']) {
              Description cannot exceed 2000 characters.
            }
          </div>
        }
      </div>

      <div class="d-flex gap-2">
        <button
          type="submit"
          class="btn btn-primary"
          [disabled]="form.invalid || saving"
        >
          @if (saving) {
            <span
              class="spinner-border spinner-border-sm me-1"
              role="status"
            ></span>
            Saving...
          } @else {
            {{ mode === 'add' ? 'Add Game' : 'Update Game' }}
          }
        </button>
        <button
          type="button"
          class="btn btn-secondary"
          [disabled]="saving"
          (click)="cancel.emit()"
        >
          Cancel
        </button>
      </div>
    </form>
  `,
})
export class GameFormComponent implements OnInit {
  private fb = inject(FormBuilder);

  @Input() initialData: GameModel | null = null;
  @Input() mode: 'add' | 'update' = 'add';
  @Input() saving = false;

  @Output() save = new EventEmitter<GameFormValue>();
  @Output() cancel = new EventEmitter<void>();

  form!: FormGroup;

  ngOnInit(): void {
    this.form = this.fb.group({
      title: [
        this.initialData?.title ?? '',
        [Validators.required, Validators.maxLength(200)],
      ],
      description: [
        this.initialData?.description ?? '',
        [Validators.maxLength(2000)],
      ],
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.form.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.save.emit(this.form.value as GameFormValue);
    }
  }
}
