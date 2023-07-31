import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { CustomerapplyloanComponent } from './Components/user/customerapplyloan/customerapplyloan.component';
import { CustomerloanstatusComponent } from './Components/user/customerloanstatus/customerloanstatus.component';
import { CustomerprofileComponent } from './Components/user/customerprofile/customerprofile.component';
import { AdminappliedloanComponent } from './Components/admin/adminappliedloan/adminappliedloan.component';
import { AdminapprovedloanComponent } from './Components/admin/adminapprovedloan/adminapprovedloan.component';
import { AdmindashboardComponent } from './Components/admin/admindashboard/admindashboard.component';
import { UserdashboardComponent } from './Components/user/userdashboard/userdashboard.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DocumentComponent } from './Components/user/customerapplyloan/document/document.component';
import { ToastrModule, ToastNoAnimationModule } from 'ngx-toastr';

//import {MatIconModule} from '@angular/material/icon';
//import { library } from '@fortawesome/fontawesome-svg-core';
@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LoginComponent,
    CustomerapplyloanComponent,
    CustomerloanstatusComponent,
    CustomerprofileComponent,
    AdminappliedloanComponent,
    AdminapprovedloanComponent,
    AdmindashboardComponent,
    UserdashboardComponent,
    DocumentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,FormsModule, BrowserAnimationsModule,HttpClientModule,ReactiveFormsModule, CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    ToastNoAnimationModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule {
 
 }
