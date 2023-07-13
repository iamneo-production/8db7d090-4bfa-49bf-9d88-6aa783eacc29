import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoanService {
    private addloanURL:string = "https://localhost:7059/user/addLoan";
    private updateloanUrl:string  = "https://localhost:7059/user/editLoan/{loanId}";
    private getloanURL:string  = "https://localhost:7059/user/viewLoan";
    private cancelloanURL:string  = "https://localhost:7059/user/deleteLoan/{loanId}";
    private adddocumentsURL:string  = "https://localhost:7059/user/addDocuments";
    private getdocumentsURL:string  = "https://localhost:7059/user/getDocuments";
    private updatedocumentsURL:string  = "https://localhost:7059/user/editDocuments/{documentId}";
    private deletedocumentsURL:string  = "https://localhost:7059/user/deletedocuments/{documentId}";
    id = localStorage.getItem('email');
  

  constructor(private http:HttpClient) { }
  addloan(addobj:any){
    return this.http.post<any>('${this.addloanURL}',addobj);
  }
  
}
