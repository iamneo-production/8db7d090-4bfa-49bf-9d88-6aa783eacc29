import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
//import { ToastrService } from 'ngx-toastr';
import { LoanService } from 'src/app/Services/loan.service';


@Component({
  selector: 'app-customerapplyloan',
  templateUrl: './customerapplyloan.component.html',
  styleUrls: ['./customerapplyloan.component.css']
})
export class CustomerapplyloanComponent implements OnInit {

    constructor(private router:Router ,private fb:FormBuilder,private loan:LoanService){
    }
    applyForm!:FormGroup;

      ngOnInit(): void {
      this.applyForm=this.fb.group({
        applicantName:['',[Validators.required]],
        applicantAddress:['',[Validators.required]],
        applicantMobile:['',[Validators.required,Validators.pattern(/^[6-9]\d{9}$/)]],
        applicantEmail:['',[Validators.required,Validators.email]],
        applicantAadhaar:['',[Validators.required,Validators.pattern(/^[2-9]{1}[0-9]{11}$/)]],
        applicantPan:['',[Validators.required, Validators.pattern(/[A-Z]{5}[0-9]{4}[A-Z]{1}/)]],
        applicantSalary:['',[Validators.required]],
        loanAmountRequired:['',[Validators.required]],
        loanRepaymentMonths:['',[Validators.required,Validators.maxLength(3)]],
      });
    }
    
    applyloan() 
    {
      console.log(this.applyForm.value);

      this.loan.addLoan(this.applyForm.value).subscribe({
        next: (res => {
          this.applyForm.reset();
         // this.notif.success(res.message);
          this.router.navigate(['user/addDocuments']);
        }),
        error: (err => {
          alert(err?.error.message);
        })
      })
    }
    
}
