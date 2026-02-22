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
  templateUrl: './game-form.component.html',
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
