import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerapplyformComponent } from './customerapplyform.component';

describe('CustomerapplyformComponent', () => {
  let component: CustomerapplyformComponent;
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule], 
    providers: [CustomerapplyformComponent]
  }));
  beforeEach(() => {
    const fixture = TestBed.createComponent(CustomerapplyformComponent);
    component = fixture.componentInstance;
  });
  it('FE_customerApplyFormTest', () => {
    expect(component).toBeTruthy();
  });
});