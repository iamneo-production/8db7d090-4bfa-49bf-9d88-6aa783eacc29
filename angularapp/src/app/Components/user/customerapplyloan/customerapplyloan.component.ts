import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoanService } from 'src/app/Services/loan.service';


export interface Applyloan {
  loanId: number;
  loantype: string;
  applicantName: string;
  applicantAddress: string;
  applicantMobile: string;
  applicantEmail: string;
  applicantAadhaar: string;
  applicantPan: string;
  applicantSalary: string;
  loanAmountRequired: string;
  loanRepaymentMonths: string;
}
 @Component({
  selector: 'app-customerapplyloan',
   templateUrl: './customerapplyloan.component.html',
   styleUrls: ['./customerapplyloan.component.css']
 })
// import statements...


export class CustomerapplyloanComponent implements OnInit {
  applyForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private loanService: LoanService) {
    this.applyForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      Mobilenumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      email: ['', [Validators.required, Validators.email]],
      Aadhaar: ['', [Validators.required, Validators.minLength(12), Validators.maxLength(12)]],
      PAN: ['', [Validators.required, Validators.maxLength(10)]],
      Salary: ['', Validators.required],
      loanamt: ['', Validators.required],
      months: ['', Validators.required],
      file: ['', Validators.required]
    });
  }

  ngOnInit() {}

  applyloan(){
    if (this.applyForm.valid) {
      const loanData: Applyloan = {
        loanId: 1, // Fill in the appropriate value
        loantype: '', // Fill in the appropriate value
        applicantName: this.applyForm.value.Name,
        applicantAddress: this.applyForm.value.Address,
        applicantMobile: this.applyForm.value.Mobilenumber,
        applicantEmail: this.applyForm.value.email,
        applicantAadhaar: this.applyForm.value.Aadhaar,
        applicantPan: this.applyForm.value.PAN,
        applicantSalary: this.applyForm.value.Salary,
        loanAmountRequired: this.applyForm.value.loanamt,
        loanRepaymentMonths: this.applyForm.value.months
      };

      // Call the loan service to add the loan
      this.loanService.addloan(loanData).subscribe(
        (response) => {
          console.log('Loan application submitted successfully.', response);
          // Reset the form after successful submission
          this.applyForm.reset();
        },
        (error) => {
          console.error('Failed to submit loan application.', error);
        }
      );
    }
  }
}
