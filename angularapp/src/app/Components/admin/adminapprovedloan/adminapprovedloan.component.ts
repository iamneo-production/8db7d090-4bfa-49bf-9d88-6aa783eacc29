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
  allLoans: LoanApplicant[] = []; // To store the original fetched loans
  filteredApplicantarray: LoanApplicant[] = []; // To store the filtered data

  ngOnInit(): void {
    this.fetchAppliedLoans();
  }

  fetchAppliedLoans() {
    this.adminService.getAllLoan().subscribe(
      response => {
        console.log(response);
        this.Applicantarray = response;
        this.allLoans = response; // Store all loans in a separate array
        this.filteredApplicantarray = response; // Initially, set filtered array to all loans
      }
    );
  }

  search() {
    if (this.searchQuery) {
      this.filteredApplicantarray = this.allLoans.filter(item => {
        // Filter the data based on your desired criteria (Loan ID or Name)
        const loanId = (item.loanId || '').toString().toLowerCase();
        const applicantPan = (item.applicantPan || '').toString().toLowerCase();
        const applicantAddress = (item.applicantAddress || '').toString().toLowerCase();
        const loanAmountRequired = (item.loanAmountRequired || '').toString().toLowerCase();
        const applicantEmail = (item.applicantEmail || '').toString().toLowerCase();
        const applicantName = (item.applicantName || '').toString().toLowerCase();
        const applicantMobile = (item.applicantMobile || '').toString().toLowerCase();
        const applicantAadhaar = (item.applicantAadhaar || '').toString().toLowerCase();
  
        return (
          loanId.includes(this.searchQuery.toLowerCase()) ||
          applicantName.includes(this.searchQuery.toLowerCase()) ||
          applicantPan.includes(this.searchQuery.toLowerCase()) ||
          applicantAddress.includes(this.searchQuery.toLowerCase()) ||
          loanAmountRequired.includes(this.searchQuery.toLowerCase()) ||
          applicantEmail.includes(this.searchQuery.toLowerCase()) ||
          applicantMobile.includes(this.searchQuery.toLowerCase())||
          applicantAadhaar.includes(this.searchQuery.toLowerCase())
          // Add more conditions for other fields if needed
        );
      });
    } else {
      // If the search query is empty, reset to show all loans
      this.filteredApplicantarray = this.allLoans;
    }
  }
  fetchAllLoans() {
    this.searchQuery = ""; // Clear the search query
    this.filteredApplicantarray = this.allLoans; // Display all the loans again
  }
}
