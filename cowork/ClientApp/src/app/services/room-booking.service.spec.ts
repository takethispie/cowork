import { TestBed } from '@angular/core/testing';

import { RoomBookingService } from './room-booking.service';

describe('RoomBookingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoomBookingService = TestBed.get(RoomBookingService);
    expect(service).toBeTruthy();
  });
});
