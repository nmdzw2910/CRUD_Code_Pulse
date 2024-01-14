import { Component, Input, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'base-input-field',
  templateUrl: './base-input-field.component.html',
  styleUrls: ['./base-input-field.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => BaseInputFieldComponent),
      multi: true,
    },
  ],
})
export class BaseInputFieldComponent implements ControlValueAccessor {
  @Input() label: string = '';
  @Input() type: string = 'text';
  @Input() inputId: string = '';
  @Input() name: string = '';
  @Input() isDisabled: boolean = false;
  @Input() required: boolean = false;
  @Input() value: any;
  onChange: any = () => {};
  onTouch: any = () => {};

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  updateValue(event: any): void {
    this.value = event.target.value;
    this.onChange(this.value);
    this.onTouch();
  }
}
