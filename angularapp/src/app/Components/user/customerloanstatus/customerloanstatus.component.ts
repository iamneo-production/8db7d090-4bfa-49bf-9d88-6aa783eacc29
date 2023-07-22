import { Component } from '@angular/core';
import { LoanService } from 'src/app/Services/loan.service';
import {applyLoan} from 'src/app/Models/applyLoan';
import { FormGroup, FormBuilder ,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { docFormat } from 'src/app/Models/document';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-customerloanstatus',
  templateUrl: './customerloanstatus.component.html',
  styleUrls: ['./customerloanstatus.component.css']
})

export class CustomerloanstatusComponent {

  //--------------------------------------------------------------------------------------------------------------------------------------------
                                                          //ADD EDIT VIEW DELETE  -> LOAN
  editLoan!:FormGroup ;
  editDocuments!:FormGroup;

  constructor(private fb: FormBuilder,private notif:ToastrService,public loanService: LoanService,private router:Router,private http :HttpClient){
    this.editLoan=this.fb.group({
      loanId:[''],
      applicantName:['',[Validators.required]],
      applicantAddress:['',[Validators.required]],
      applicantMobile:['',[Validators.required,Validators.pattern(/^[6-9]\d{9}$/),Validators.minLength(10),Validators.maxLength(10)]],
      applicantEmail:['',[Validators.required,Validators.email]],    
      applicantAadhaar:['',[Validators.required,Validators.pattern(/^[2-9]{1}[0-9]{11}$/),Validators.minLength(12),Validators.maxLength(12)]],
      applicantPan:['',[Validators.required, Validators.pattern(/[A-Z]{5}[0-9]{4}[A-Z]{1}/)]],
      applicantSalary:['',[Validators.required]],
      loanAmountRequired:['',[Validators.required]],
      loanRepaymentMonths:['',[Validators.required,Validators.maxLength(3)]],
    });

    this.editDocuments=this.fb.group({
      documentid:[],
      documenttype:[],
      documentupload:[],
    });
  }
  
  Show:boolean=true;
  Display:boolean=false;
  edit:boolean=false;
  oneditTime=false;
  OnEditdoc:boolean=false;

  ngOnInit():void {}

  loanid:string='';
  loanData:applyLoan[]=[];
    
  getById(){
    this.Show=false;
    this.getLoan();
    this.edit=true;
    this.getDocs();
  }

  getLoan()
  {
    this.loanService.viewLoan(this.loanid).subscribe({
      next: (res => {
        this.loanData=[res];
        this.Display=true;
        console.log(this.loanData);
        
      })
      , error: (err => {
        this.notif.error(err?.error.message);
        alert(err?.error.message)
      })
    })
    }

    onedit() {
      this.loanService.editLoan(this.editLoan.value).subscribe((Response: any) =>{
          console.log(Response);
          this.edit=true;
          this.oneditTime=false;
          this.getLoan();
      });
    }

    updateform(loan: applyLoan) {
      console.log(loan)
      this.edit=false;
      this.Display=false;
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

    ondelete(id:string){
      console.log(id)
      this.loanService.deleteLoan(id).subscribe((res: any)=>{
        console.log(res);
        alert("Application Deleted");
        this.router.navigate(['user/addLoan']);
      }); 
    }

    scrollPageToTop() {
      this.edit=false;
      window.scrollTo({ top: 0, behavior: 'smooth' });

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
                                                      //GET - EDIT - DELETE  - DOCUMENT
    
    documentid:string="";                                                  
    docArray:docFormat[]=[];
    getDocs(){

      this.loanService.getDocuments(this.documentid).subscribe({
        next: (res => {
          console.log(res);
          this.docArray=[res];
          this.byteArray=this.docArray[0].documentupload;
          console.log(this.byteArray);
        })
        , error: (err => {
          alert(err?.error.message)
        })
        })
    }

    byteArray: any;
    downloadPDF () {
    
    console.log(this.byteArray);
    const blob = new Blob ( [this.byteArray], { type: "application/pdf" });
    // Create a URL for the blob
    const url = URL.createObjectURL (blob);
    // Get the <a> element using querySelector and provide a type parameter
    const link = document.querySelector<HTMLAnchorElement> ("#download");
    // Assign the URL to the href attribute
    link!.href = url;
    
    }

    updateDoc( doc:docFormat){
      this.OnEditdoc=true;
      this.editDocuments.setValue({
        documentid:doc.documentId,
        documenttype:doc.documenttype,
        documentupload:doc.documentupload
      })
    }

    selectedFile!:File;

    onFileSelected(event: any) {
      this.selectedFile = event.target.files[0];
    }
    

    ondeleteDoc(doc_id:number){
      console.log(doc_id)
      this.loanService.deleteDocuments(doc_id).subscribe((res: any)=>{
        console.log(res);
        alert("Application Deleted");
        this.router.navigate(['user/addDocuments']);
      }); 
    }

    onUpdateDoc() {
      
      const headers = new HttpHeaders();
      headers.append('Content-Type', 'multipart/form-data'); // Set the correct media type here
    
      this.http.put('https://localhost:7064/user/addDocuments/${documentData.doc_id}',this.editDocuments , { headers }).subscribe(

        (Response: any) => {
          alert("Document Uploaded")
          console.log('Document uploaded successfully!', Response);
        },
        (error: any) => {
          console.error('Error uploading document:', error);
        });
    }
}
