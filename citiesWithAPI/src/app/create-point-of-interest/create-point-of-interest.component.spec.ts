import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePointOfInterestComponent } from './create-point-of-interest.component';

describe('CreatePointOfInterestComponent', () => {
  let component: CreatePointOfInterestComponent;
  let fixture: ComponentFixture<CreatePointOfInterestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatePointOfInterestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePointOfInterestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
