import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseCarouselComponent } from './base-carousel.component';

describe('BaseCarouselComponent', () => {
  let component: BaseCarouselComponent;
  let fixture: ComponentFixture<BaseCarouselComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BaseCarouselComponent]
    });
    fixture = TestBed.createComponent(BaseCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
