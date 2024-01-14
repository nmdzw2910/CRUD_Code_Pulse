import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseInputFieldComponent } from './base-input-field.component';

describe('BaseInputFieldComponent', () => {
  let component: BaseInputFieldComponent;
  let fixture: ComponentFixture<BaseInputFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BaseInputFieldComponent]
    });
    fixture = TestBed.createComponent(BaseInputFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
