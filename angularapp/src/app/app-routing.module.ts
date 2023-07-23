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
import { AuthGuard } from './Guards/auth.guard';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'user/addLoan',component:CustomerapplyloanComponent,canActivate:[AuthGuard]},
  {path:'user/viewLoan',component:CustomerloanstatusComponent,canActivate:[AuthGuard]},
  {path:'user/getProfile',component:CustomerprofileComponent,canActivate:[AuthGuard]},
  {path:'user',component:UserdashboardComponent,canActivate:[AuthGuard]},
  {path:'admin/getAllLoans',component:AdminappliedloanComponent,canActivate:[AuthGuard]},
  {path:'admin/GetAllLoans',component:AdminapprovedloanComponent,canActivate:[AuthGuard]},
  {path:'admin',component:AdmindashboardComponent,canActivate:[AuthGuard]},
  {path:'addDocuments',component:DocumentComponent,canActivate:[AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
