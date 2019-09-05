import { TestBed } from '@angular/core/testing';

import { TicketAttributionService } from './ticket-attribution.service';

describe('TicketAttributionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketAttributionService = TestBed.get(TicketAttributionService);
    expect(service).toBeTruthy();
  });
});
