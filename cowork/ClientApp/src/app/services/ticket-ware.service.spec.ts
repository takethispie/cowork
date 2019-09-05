import { TestBed } from '@angular/core/testing';

import { TicketWareService } from './ticket-ware.service';

describe('TicketWareService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketWareService = TestBed.get(TicketWareService);
    expect(service).toBeTruthy();
  });
});
