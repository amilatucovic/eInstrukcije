import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {

  @Input() message: string = '';
  @Input() show: boolean = false;
  @Input() iconClass: string = 'bi-trash-fill';
  @Input() iconColorClass: string = 'bg-danger';
  @Input() title: string = 'Potvrda akcije';
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();



  onConfirm(): void {
    this.confirm.emit();
    this.show = false;
  }

  onCancel(): void {
    this.cancel.emit();
    this.show = false;
  }
}
