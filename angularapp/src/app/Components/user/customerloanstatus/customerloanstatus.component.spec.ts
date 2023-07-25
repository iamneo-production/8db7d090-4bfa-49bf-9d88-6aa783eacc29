import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerloanstatusComponent } from './customerloanstatus.component';

describe('CustomerloanstatusComponent', () => {
  let component: CustomerloanstatusComponent;
  let fixture: ComponentFixture<CustomerloanstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerloanstatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerloanstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
