import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './Components/signup/signup.component';
import { LoginComponent } from './Components/login/login.component';
import { CustomerapplyloanComponent } from './Components/user/customerapplyloan/customerapplyloan.component';
import { CustomerloanstatusComponent } from './Components/user/customerloanstatus/customerloanstatus.component';
import { CustomerprofileComponent } from './Components/user/customerprofile/customerprofile.component';
import { AdminappliedloanComponent } from './Components/admin/adminappliedloan/adminappliedloan.component';
import { AdmindashboardComponent } from './Components/admin/admindashboard/admindashboard.component';
import { UserdashboardComponent } from './Components/user/userdashboard/userdashboard.component';
import { AdminapprovedloanComponent } from './Components/admin/adminapprovedloan/adminapprovedloan.component';
import { DocumentComponent } from './Components/user/customerapplyloan/document/document.component';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'user/addLoan',component:CustomerapplyloanComponent},
  {path:'user/viewLoan',component:CustomerloanstatusComponent},
   {path:'user/getProfile',component:CustomerprofileComponent},
  {path:'user',component:UserdashboardComponent},
  {path:'documents',component:DocumentComponent},
  {path:'admin/getAllLoans',component:AdminappliedloanComponent},
  {path:'admin/Loandetails',component:AdminapprovedloanComponent},
  {path:'admin',component:AdmindashboardComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
