import { TestBed } from '@angular/core/testing';

import { LoanService } from './loan.service';
//import { Applyloan } from '../helpers/customerapplyloan';

describe('LoanService', () => {
  let service: LoanService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoanService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
