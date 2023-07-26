import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerapplyloanComponent } from './customerapplyloan.component';

describe('CustomerapplyloanComponent', () => {
  let component: CustomerapplyloanComponent;
  let fixture: ComponentFixture<CustomerapplyloanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerapplyloanComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerapplyloanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
