import { TestBed } from '@angular/core/testing';

import { MealBookingService } from './meal-booking.service';

describe('MealBookingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MealBookingService = TestBed.get(MealBookingService);
    expect(service).toBeTruthy();
  });
});
