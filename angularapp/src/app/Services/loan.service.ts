import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { applyLoan } from '../Models/applyLoan';
import { docFormat } from '../Models/document';
import { signupForm } from '../Models/signupForm';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  constructor(private http:HttpClient) { }

  private baseUrl:string="https://localhost:7064";
  
  addLoan(loanData: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/user/addLoan`, loanData);
  }

  editLoan(editloan:applyLoan):Observable<applyLoan> {
    return this.http.put<applyLoan>(`${this.baseUrl}/user/editLoan/${editloan.loanId}`,editloan);
  }

  viewLoan(loanId: string) : Observable<applyLoan>  {
    return this.http.get<applyLoan>(`${this.baseUrl}/user/viewLoan/${loanId}`);
  }

 /* viewLoan(loanId: string, loanData: any) : Observable<any>  {
    return this.http.get(`${this.baseUrl}/user/viewLoan/${loanId}`,loanData);
  } */

  deleteLoan(loanId: string): Observable<applyLoan> {
    return this.http.delete<applyLoan>(`${this.baseUrl}/user/deleteLoan/${loanId}`);
  }

  addDocuments(documentData: any):Observable<any> {
    return this.http.post(`${this.baseUrl}/user/addDocuments`, documentData);
  }

  getDocuments(documentid:string):Observable<docFormat>{
    return this.http.get<docFormat>(`${this.baseUrl}/user/getDocuments/${documentid}`);
  }

  // editDocuments(documentData: any):Observable<docFormat> {
  //   return this.http.put<docFormat>(`${this.baseUrl}/user/editDocuments/${documentData.doc_id}`, documentData);
  // }

  deleteDocuments(documentId: number) {
    return this.http.delete(`${this.baseUrl}/user/deleteDocuments/${documentId}`);
  }

  getProfile(data:string):Observable<signupForm>{
    return this.http.get<signupForm>(`${this.baseUrl}/user/getProfile/${data}`);
  }
  editProfile(editUser:signupForm):Observable<signupForm> {
    return this.http.put<signupForm>(`${this.baseUrl}/user/editProfile/${editUser.id}`,editUser);
  }
  deleteProfile(UserID: string): Observable<signupForm> {
    return this.http.delete<signupForm>(`${this.baseUrl}/user/deleteProfile/${UserID}`);
  }

}
