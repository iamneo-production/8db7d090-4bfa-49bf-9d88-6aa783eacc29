import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoanstatusComponent } from './loanstatus.component';

describe('LoanstatusComponent', () => {
  let component: LoanstatusComponent;
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule], 
    providers: [LoanstatusComponent]
  }));
  beforeEach(() => {
    const fixture = TestBed.createComponent(LoanstatusComponent);
    component = fixture.componentInstance;
  });
  it('FE_loanStatusTest', () => {
    expect(component).toBeTruthy();
  });
});