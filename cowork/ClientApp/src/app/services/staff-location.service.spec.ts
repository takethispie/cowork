import { TestBed } from '@angular/core/testing';

import { StaffLocationService } from './staff-location.service';

describe('StaffLocationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StaffLocationService = TestBed.get(StaffLocationService);
    expect(service).toBeTruthy();
  });
});
