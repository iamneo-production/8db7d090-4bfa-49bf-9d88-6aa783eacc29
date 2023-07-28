import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { signupForm } from 'src/app/Models/signupForm';
import { LoanService } from 'src/app/Services/loan.service';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormGroup ,Validators} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customerprofile',
  templateUrl: './customerprofile.component.html',
  styleUrls: ['./customerprofile.component.css']
})
export class CustomerprofileComponent implements OnInit {
  // faTriangleExclamation = faExclamationTriangle;
  // faEnvelope = faEnvelope;
  // faLock = faLock;
  // faEye = faEye;
  // faEyeSlash = faEyeSlash;
  showPass: boolean = false;
  showConfirmPass: boolean = false;
  passType: string = 'password';
  confirmPassType: string = 'password';

  constructor(private auth:AuthService ,private loanService:LoanService,private fb:FormBuilder,private router:Router){
    this.editUser=this.fb.group({
      id:[''],
      userRole:[''],
      email:['',[Validators.required,Validators.email]],
      userName:['',[Validators.required]],
      mobileNumber:['',[Validators.required,Validators.pattern(/^[6-9]\d{9}$/)]],
      password:['',Validators.required],
      confirmpassword:['', Validators.required]
    });
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
   toggleConfirmPass() {
    // if(this.signupForm.get('password')?.value!==this.signupForm.get('confirmpassword')?.value){
    //   this.display=true;
    // }
    if (this.showConfirmPass) {
      this.showConfirmPass = false;
      this.confirmPassType = 'password';
    } else {
      this.showConfirmPass = true;
      this.confirmPassType = 'text';
    }
  }
  profileData:signupForm[]=[];
  editUser!:FormGroup ;
  ID!:string;
  show:boolean=true;
  display:boolean=false;

 
  ngOnInit(){
       this.fetchData();
  }
 
  fetchData(){
    this.ID=this.auth.getID();
    console.log(this.ID)
      this.loanService.getProfile(this.ID).subscribe({
        next: (res => {
          console.log(res);
          this.profileData=[res];
          console.log(this.profileData);
        })
        , error: (err => {
          alert(err?.error.message)
        })
      })
  }

 onupdate(){
  this.show=true;
  this.display=false;
  console.log(this.editUser.value);
  this.loanService.editProfile(this.editUser.value).subscribe((Response: any) =>{
    console.log(Response);
    this.fetchData();
});

 }

  onedit(prof:signupForm)
  {
    this.show=false;
    this.display=true;
    console.log(this.auth.getID());
    this.editUser.setValue({
      id:prof.id,
      userRole:prof.userRole,
      email:prof.email,
      userName:prof.userName,
      mobileNumber:prof.mobileNumber,
      password:prof.password,
      confirmpassword:prof.confirmpassword,
    })

  }
  ondel(){
    this.loanService.deleteProfile(this.auth.getID()).subscribe((res: any)=>{
      console.log(res);
      alert("Application Deleted");
      this.router.navigate(['user']);
    }); 
  }


scrollPageToTop() {
  window.scrollTo({ top: 0, behavior: 'smooth' });

}

}