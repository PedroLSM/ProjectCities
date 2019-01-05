import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PointOfInterestComponent } from './point-of-interest.component';

describe('PointOfInterestComponent', () => {
  let component: PointOfInterestComponent;
  let fixture: ComponentFixture<PointOfInterestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PointOfInterestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PointOfInterestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
