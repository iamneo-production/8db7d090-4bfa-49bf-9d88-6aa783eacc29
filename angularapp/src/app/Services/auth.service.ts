import { Injectable } from '@angular/core';
import {HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private adminloginurl:string="https://localhost:7059/admin/login"
  private adminsignupurl:string="https://localhost:7059/admin/signup"
  private userloginurl:string="https://localhost:7059/user/login"
  private usersignupurl:string="https://localhost:7059/user/signup"
  private applyloanurl:string="https://localhost:7059/user/addLoan"
  constructor(private http:HttpClient) { }
  adminsignup(adminobj:any){
    return this.http.post<any>(`${this.adminsignupurl}`,adminobj);
  }

   usersignup(userobj:any){
     return this.http.post<any>(`${this.usersignupurl}`,userobj);
  }

   adminlogin(adminobj:any ){
    return this.http.post<any>(`${this.adminloginurl}`,adminobj);
  }
  userlogin(loginobj:any ){
       return this.http.post<any>(`${this.userloginurl}`,loginobj);
  }
  applyloans(loanobj:any){
    return this.http.post<any>(`${this.applyloanurl}`,loanobj);
  }
}
