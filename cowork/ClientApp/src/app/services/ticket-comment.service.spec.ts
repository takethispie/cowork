import { TestBed } from '@angular/core/testing';

import { TicketCommentService } from './ticket-comment.service';

describe('TicketCommentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TicketCommentService = TestBed.get(TicketCommentService);
    expect(service).toBeTruthy();
  });
});
