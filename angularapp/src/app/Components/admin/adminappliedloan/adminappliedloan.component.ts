import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/Services/admin.service';
import { HttpClient } from '@angular/common/http';
import { LoanApplicant } from 'src/app/Models/LoanApplicant';
import { FormBuilder, FormGroup } from '@angular/forms';
import { applyLoan } from 'src/app/Models/applyLoan';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminappliedloan',
  templateUrl: './adminappliedloan.component.html',
  styleUrls: ['./adminappliedloan.component.css']
})
export class AdminappliedloanComponent implements OnInit {
  Applicantarray: applyLoan[] = [];
  item: applyLoan[] = []; // Define the 'item' variable as an array of LoanApplicant objects
  editLoan!:FormGroup;
  selectedApplicants: applyLoan[] = [];
  constructor(private fb:FormBuilder  ,private adminService: AdminService, private http: HttpClient, private router:Router) {

    this.editLoan=this.fb.group({
      loanId:[''],
      applicantName:[''],
      applicantAddress:[''],
      applicantMobile:[''],
      applicantEmail:[''],    
      applicantAadhaar:[''],
      applicantPan:[''],
      applicantSalary:[''],
      loanAmountRequired:[''],
      loanRepaymentMonths:[''],
    });
   }

  ngOnInit(): void {
    this.fetchAppliedLoans();
  }

  fetchAppliedLoans() {
    this.adminService.getAllLoan().subscribe(
      response => {
        console.log(response);
        this.Applicantarray =response;
        console.log(this.Applicantarray);
      }
    );
  }

  handleApprove(loanId: number) {
    this.adminService.approveLoan(loanId).subscribe(
      (res: any) => {
        if (res.Status === 'Success') {  
          console.log(res.Status)
          this.Show=false;
          this.Show=true;
          this.ngOnInit();
        } else {
          console.log(res)
        }
      }, (err) => console.log(err));
  }

  handleReject(loanId: number) {
    this.adminService.rejectLoan(loanId).subscribe(
      (res:any)=>
    {
      if (res.Status === 'Success') {  
        this.ngOnInit();
      } else {
        console.log(res)
      }
    }, (err) => console.log(err));
}



handleMultipleApprove() {
  const selectedIds = this.selectedApplicants.map((applicant) => applicant.loanId);
  this.adminService.approveMultipleLoans(selectedIds.map(Number)).subscribe(
    (res: any) => {
      if (res.Status === 'Success') {
        console.log(res.Status);
        this.selectedApplicants = []; // Clear the selected applicants
        this.ngOnInit();
      } else {
        console.log(res);
      }
    },
    (err: any) => console.log(err)
  );
}

handleMultipleReject() {
  const selectedIds = this.selectedApplicants.map((applicant) => applicant.loanId);
  this.adminService.rejectMultipleLoans(selectedIds.map(Number)).subscribe(
    (res: any) => {
      if (res.Status === 'Success') {
        this.selectedApplicants = []; // Clear the selected applicants
        this.ngOnInit();
      } else {
        console.log(res);
      }
    },
    (err: any) => console.log(err)
  );
}

handleSelection(applicant: applyLoan) {
  if (applicant.selected) {
    this.selectedApplicants.push(applicant);
  } else {
    const index = this.selectedApplicants.indexOf(applicant);
    if (index > -1) {
      this.selectedApplicants.splice(index, 1);
    }
  }
}


    //Edit Loan

    
    Show:boolean=true;
    oneditTime:boolean=false;
    

    onedit() {
      this.adminService.editLoan(this.editLoan.value).subscribe((Response: any) =>{
          console.log(Response);
          this.Show=true;
          this.oneditTime=false;
          this.fetchAppliedLoans();
      });
    }

    adminEditLoan(loan: applyLoan) {
      console.log(loan);
      this.Show=false;
      this.oneditTime=true;
      this.editLoan.setValue({
        loanId:loan.loanId,
        applicantName:loan.applicantName,
        applicantAddress:loan.applicantAadhaar,
        applicantMobile:loan.applicantMobile,
        applicantEmail:loan.applicantEmail,
        applicantAadhaar:loan.applicantAadhaar,
        applicantPan:loan.applicantPan,
        applicantSalary:loan.applicantSalary,
        loanAmountRequired:loan.loanAmountRequired,
        loanRepaymentMonths:loan.loanRepaymentMonths,
      })
    }

    adminDeleteLoan(id:number){
      console.log(id)
      this.adminService.deleteLoan(id).subscribe((res: any)=>{
        console.log(res);
        alert("Application Deleted");
        this.fetchAppliedLoans();
      }); 
    }

}