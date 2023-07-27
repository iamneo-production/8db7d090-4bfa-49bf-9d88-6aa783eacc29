import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoanService } from 'src/app/Services/loan.service';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css'],
})
export class DocumentComponent implements OnInit {
  documentForm!: FormGroup;
  showid: boolean = false;
  selectedFile!: File;
  loan_id:string='';
  doc:boolean=true;

  constructor(private fb: FormBuilder, private loan: LoanService, private router: Router, private http: HttpClient) {}

  ngOnInit(): void {
    this.documentForm = this.fb.group({
      documenttype: ['', Validators.required],
      documentupload: [null, Validators.required]
    });
  }
  
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
  
  onSubmit() {
    if (this.documentForm.invalid || !this.selectedFile?.name) {
      return;
    }
console.log(this.documentForm.value)
    const formData = new FormData();
    formData.append('documenttype', this.documentForm.get('documenttype')!.value);

    if (this.selectedFile) {
      formData.append('DocumentUploads', this.selectedFile, this.selectedFile.name);
    }
  
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data'); // Set the correct media type here
  
    this.http.post('https://8080-ededcfcebccebccbdddbcadfdcbadbeccadadabbe.project.examly.io/user/addDocuments', formData, { headers }).subscribe(
      (response) => {
        alert("Document Uploaded")
        this.showid=true;
        this.doc=false;
        this.loan_id=JSON.stringify(response)
        console.log('Document uploaded successfully! ', response);
      },
      (error) => {
        console.error('Error uploading document:', error);
      }
    );
  
    this.showid = true;
    this.doc = false;
  }
}
