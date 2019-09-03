import { TestBed } from '@angular/core/testing';

import { TimeSlotService } from './time-slot.service';

describe('TimeSlotService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimeSlotService = TestBed.get(TimeSlotService);
    expect(service).toBeTruthy();
  });
});
