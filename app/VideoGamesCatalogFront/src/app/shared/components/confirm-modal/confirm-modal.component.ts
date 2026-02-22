import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  template: `
    <div class="modal-header">
      <h5 class="modal-title">{{ title }}</h5>
      <button
        type="button"
        class="btn-close"
        aria-label="Close"
        (click)="modal.dismiss()"
      ></button>
    </div>
    <div class="modal-body">
      <p>{{ message }}</p>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" (click)="modal.dismiss()">
        Cancel
      </button>
      <button type="button" class="btn btn-danger" (click)="modal.close(true)">
        {{ confirmText }}
      </button>
    </div>
  `,
})
export class ConfirmModalComponent {
  modal = inject(NgbActiveModal);

  @Input() title = 'Confirm';
  @Input() message = 'Are you sure?';
  @Input() confirmText = 'Confirm';
}
