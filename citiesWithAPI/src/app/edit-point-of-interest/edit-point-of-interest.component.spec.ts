import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPointOfInterestComponent } from './edit-point-of-interest.component';

describe('EditPointOfInterestComponent', () => {
  let component: EditPointOfInterestComponent;
  let fixture: ComponentFixture<EditPointOfInterestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPointOfInterestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPointOfInterestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
