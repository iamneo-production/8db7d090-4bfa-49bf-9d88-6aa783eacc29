import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerreviewComponent } from './customerreview.component';

describe('CustomerreviewComponent', () => {
  let component: CustomerreviewComponent;
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule], 
    providers: [CustomerreviewComponent]
  }));
  beforeEach(() => {
    const fixture = TestBed.createComponent(CustomerreviewComponent);
    component = fixture.componentInstance;
  });
  it('FE_customerReviewTest', () => {
    expect(component).toBeTruthy();
  });
});