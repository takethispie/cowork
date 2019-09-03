import { TestBed } from '@angular/core/testing';

import { SubscriptionTypeService } from './subscription-type.service';

describe('SubscriptionTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SubscriptionTypeService = TestBed.get(SubscriptionTypeService);
    expect(service).toBeTruthy();
  });
});
