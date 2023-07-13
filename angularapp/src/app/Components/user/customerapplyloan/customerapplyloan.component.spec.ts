import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerapplyloanComponent } from './customerapplyloan.component';

describe('CustomerapplyloanComponent', () => {
  let component: CustomerapplyloanComponent;
  let fixture: ComponentFixture<CustomerapplyloanComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerapplyloanComponent]
    });
    fixture = TestBed.createComponent(CustomerapplyloanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
