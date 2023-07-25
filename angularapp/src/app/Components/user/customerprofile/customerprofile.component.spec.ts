import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerprofileComponent } from './customerprofile.component';

describe('CustomerprofileComponent', () => {
  let component: CustomerprofileComponent;
  let fixture: ComponentFixture<CustomerprofileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerprofileComponent]
    });
    fixture = TestBed.createComponent(CustomerprofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
