import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoanApplicant } from '../Models/LoanApplicant';
import { Observable } from 'rxjs';
import { applyLoan } from '../Models/applyLoan';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  private baseUrl:string="https://8080-ededcfcebccebccbdddbcadfdcbadbeccadadabbe.project.examly.io";

  getAllLoan() : Observable<any>{
    return this.http.get(`${this.baseUrl}/admin/getAllLoans`);
  }
  approveLoan(loanId: number) {
    return this.http.post(`${this.baseUrl}/admin/getAllLoans/approve/${loanId}`, { loanId });
  }
  
  rejectLoan(loanId: number) {
    return this.http.post(`${this.baseUrl}/admin/getAllLoans/reject/${loanId}`, { loanId });
  }
  approveMultipleLoans(loanIds: number[]): Observable<any> {
    return this.http.put(`${this.baseUrl}/approve`, loanIds);
  }

  rejectMultipleLoans(loanIds: number[]): Observable<any> {
    return this.http.put(`${this.baseUrl}/reject`, loanIds);
  }
 
  SearchName(name:string):Observable<LoanApplicant>{
    return this.http.get<LoanApplicant>(`${this.baseUrl}/admin/getLoans/${name}`);
  }
  

  SearchId(loanId:string):Observable<LoanApplicant>{
    return this.http.get<LoanApplicant>(`${this.baseUrl}/admin/getAllLoans/${loanId}`);
  }

  editLoan(editloan:applyLoan):Observable<applyLoan> {
    return this.http.put<applyLoan>(`${this.baseUrl}/admin/editLoan/${editloan.loanId}`,editloan);
  }

  deleteLoan(loanId: number): Observable<applyLoan> {
    return this.http.delete<applyLoan>(`${this.baseUrl}/admin/deleteLoan/${loanId}`);
  }
  
}
