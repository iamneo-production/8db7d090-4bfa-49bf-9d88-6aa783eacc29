import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Validators,FormGroup, FormBuilder } from '@angular/forms';

import {
  faEye,
  faEyeSlash,
  faExclamationTriangle,
  faEnvelope,
  faLock}from '@fortawesome/free-solid-svg-icons';
import { AuthService } from 'src/app/Services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  constructor(private router:Router,private auth:AuthService,private fb:FormBuilder){}
  loginForm!:FormGroup;
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.email, Validators.required]],
      password: ['', Validators.required]
    });
  }
  faTriangleExclamation = faExclamationTriangle;
  faEnvelope = faEnvelope;
  faLock = faLock;
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  passType:string='password';
  showPass:boolean=false;

 onlogin(){
  if (this.loginForm.valid) {
    console.log(this.loginForm.value);
    const passwordValue = this.loginForm.get('password')?.value;
    const email = this.loginForm.get('email')?.value;
    localStorage.setItem('email',email)
    if(passwordValue==='admin'){
      this.auth.adminlogin(this.loginForm.value).subscribe({
        next: (res) => {
          alert(res.message);
            this.router.navigate(['admin']);
        },
        error: (err) => {
          alert(err?.error.message);
        }
      });
    }
    else{
      this.auth.userlogin(this.loginForm.value).subscribe({
        next: (res) => {
          alert(res.message);
            this.router.navigate(['user']);
        },
        error: (err) => {
          alert(err?.error.message);
        }
      });
    }
    
  }
 } 

 toggle() {
  if (this.showPass) {
    this.showPass = false;
    this.passType = 'password';
  } else {
    this.showPass = true;
    this.passType = 'text';
  }
}

}