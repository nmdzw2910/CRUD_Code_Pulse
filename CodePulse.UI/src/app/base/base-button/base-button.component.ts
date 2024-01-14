import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'base-button',
  templateUrl: './base-button.component.html',
  styleUrls: ['./base-button.component.css'],
})
export class BaseButtonComponent {
  @Input() type: 'submit' | 'cancel' | 'default' = 'default';
  @Input() customText: string = 'Click';
  @Input() customColor: string = 'gray';
  @Input() customClass: string = '';
  @Input() loading: boolean = false;
  @Output() buttonClick: EventEmitter<void> = new EventEmitter<void>();

  get buttonText(): string {
    if (this.type === 'submit') {
      return 'Submit';
    } else if (this.type === 'cancel') {
      return 'Cancel';
    } else {
      return this.customText;
    }
  }

  handleClick(): void {
    if (!this.loading) {
      this.buttonClick.emit();
    }
  }
}
