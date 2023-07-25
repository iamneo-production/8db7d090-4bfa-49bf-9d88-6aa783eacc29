import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { RouterTestingModule } from '@angular/router/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AdminapprovalformComponent } from './adminapprovalform.component';

describe('AdminapprovalformComponent', () => {
  let component: AdminapprovalformComponent;
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule], 
    providers: [AdminapprovalformComponent]
  }));
  beforeEach(() => {
    const fixture = TestBed.createComponent(AdminapprovalformComponent);
    component = fixture.componentInstance;
  });
  it('FE_adminApprovalFormTest', () => {
    expect(component).toBeTruthy();
  });
});