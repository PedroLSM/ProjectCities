import { TestBed } from '@angular/core/testing';

import { WindowVisibleService } from './window-visible.service';

describe('WindowVisibleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WindowVisibleService = TestBed.get(WindowVisibleService);
    expect(service).toBeTruthy();
  });
});
