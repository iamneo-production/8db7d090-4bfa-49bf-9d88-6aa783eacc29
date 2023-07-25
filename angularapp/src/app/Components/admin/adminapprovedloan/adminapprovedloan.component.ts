import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoanApplicant } from 'src/app/Models/LoanApplicant';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-adminapprovedloan',
  templateUrl: './adminapprovedloan.component.html',
  styleUrls: ['./adminapprovedloan.component.css']
})
export class AdminapprovedloanComponent implements OnInit {
  constructor(private adminService: AdminService, private Http: HttpClient) { }

  searchQuery: string = "";
  Applicantarray: LoanApplicant[] = [];
  filteredApplicantarray: LoanApplicant[] = [];

  ngOnInit(): void {
    this.fetchAppliedLoans();
  }

  fetchAppliedLoans() {
    this.adminService.getAllLoan().subscribe(
      response => {
        console.log(response);
        this.Applicantarray = response;
        this.filteredApplicantarray = response;
      }
    );
  }

  search() {
    if (this.searchQuery) {
      const searchQueryLower = this.searchQuery.toLowerCase();
      const regex = new RegExp(searchQueryLower, 'i'); 
  
      this.filteredApplicantarray = this.Applicantarray.filter(item => {
        return (
          regex.test(item.loanId?.toString()) ||
          regex.test(item.applicantName) ||
          regex.test(item.applicantPan) ||
          regex.test(item.applicantAddress) ||
          regex.test(item.loanAmountRequired?.toString()) ||
          regex.test(item.applicantEmail) ||
          regex.test(item.applicantMobile)
          
        );
      });
    } else {
   
      this.filteredApplicantarray = this.Applicantarray;
    }
  }
  
}