import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AdminhomepageComponent } from './adminhomepage.component';

describe('AdminhomepageComponent', () => {
  let component: AdminhomepageComponent;
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule], 
    providers: [AdminhomepageComponent]
  }));
  beforeEach(() => {
    const fixture = TestBed.createComponent(AdminhomepageComponent);
    component = fixture.componentInstance;
  });
  it('FE_adminHomepageTest', () => {
    expect(component).toBeTruthy();
  });
});