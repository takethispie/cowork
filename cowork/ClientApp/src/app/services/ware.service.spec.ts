import { TestBed } from '@angular/core/testing';

import { WareService } from './ware.service';

describe('WareService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WareService = TestBed.get(WareService);
    expect(service).toBeTruthy();
  });
});
