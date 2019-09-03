import { TestBed } from '@angular/core/testing';

import { WareBookingService } from './ware-booking.service';

describe('WareBookingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WareBookingService = TestBed.get(WareBookingService);
    expect(service).toBeTruthy();
  });
});
