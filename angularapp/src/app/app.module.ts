import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common'
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { AdmindashboardComponent } from './Components/admin/admindashboard/admindashboard.component';
import { UserdashboardComponent } from './Components/user/userdashboard/userdashboard.component';
import { CustomerapplyloanComponent } from './Components/user/customerapplyloan/customerapplyloan.component';
import { CustomerloanstatusComponent } from './Components/user/customerloanstatus/customerloanstatus.component';
import { CustomerprofileComponent } from './Components/user/customerprofile/customerprofile.component';
import { DocumentComponent } from './Components/user/customerapplyloan/document/document.component';
import { AdminappliedloanComponent } from './Components/admin/adminappliedloan/adminappliedloan.component';
import { AdminapprovedloanComponent } from './Components/admin/adminapprovedloan/adminapprovedloan.component';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    AdmindashboardComponent,
    UserdashboardComponent,
    CustomerapplyloanComponent,
    CustomerloanstatusComponent,
    CustomerprofileComponent,
    DocumentComponent,
    AdminappliedloanComponent,
    AdminapprovedloanComponent
  ],
  imports: [
    AppRoutingModule,FormsModule, BrowserAnimationsModule,HttpClientModule,ReactiveFormsModule, CommonModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }


/*  */