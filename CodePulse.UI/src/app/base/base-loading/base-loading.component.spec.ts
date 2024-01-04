import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseLoadingComponent } from './base-loading.component';

describe('BaseLoadingComponent', () => {
  let component: BaseLoadingComponent;
  let fixture: ComponentFixture<BaseLoadingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BaseLoadingComponent]
    });
    fixture = TestBed.createComponent(BaseLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
