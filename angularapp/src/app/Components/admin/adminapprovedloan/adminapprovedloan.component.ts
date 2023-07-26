import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoanApplicant } from 'src/app/Models/LoanApplicant';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-adminapprovedloan',
  templateUrl: './adminapprovedloan.component.html',
  styleUrls: ['./adminapprovedloan.component.css']
})
export class AdminapprovedloanComponent implements OnInit{
  constructor(private adminService:AdminService,private Http:HttpClient) {
  }
  searchQuery:string="";
  getAllLoans:boolean=true;

  Applicantarray: LoanApplicant[] = [];
  item: LoanApplicant[] = [];
 
  
  ngOnInit(): void{
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

  SearchbyId(){
    this.adminService.SearchId(this.searchQuery).subscribe(
      response => {
        console.log(response);
        this.Applicantarray =[response];
        console.log(this.Applicantarray);
      }
    );
  }
  
  SearchbyName(){
      this.adminService.SearchName(this.searchQuery).subscribe(
      response => {
        console.log(response);
        this.Applicantarray =[response];
        console.log(this.Applicantarray);
      }
    );
  }
}

