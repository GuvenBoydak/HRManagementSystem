import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class GenericHttpService {

  api = 'http://localhost:44300/api'; 
  headers :any; 
  constructor(
    private _http: HttpClient,
    private _spinner: NgxSpinnerService,
    private _toastr: ToastrService
  ) {
    this.headers = {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`
   }
  }

  get<T>(url:string,callback:(res:T)=> void){
    this._spinner.show();
    this._http.get<T>(`${this.api}/${url}`, { headers: this.headers }).subscribe({
      next: (res:T)=> {
        callback(res);
        this._spinner.hide();
      },
      error: (err:HttpErrorResponse)=> {
        console.log(err)
        this._toastr.error(err.error.message)
        this._spinner.hide();
      }
    })
  }

  post<T>(url:string,model:any,callback:(res: T)=> void){
    this._spinner.show();
    this._http.post<T>(`${this.api}/${url}`,model, { headers: this.headers }).subscribe({
      next: (res:T)=> {
        callback(res);
        this._spinner.hide();
      },
      error: (err:HttpErrorResponse)=> {
        console.log(err)
        this._toastr.error(err.error.message)
        this._spinner.hide();
      }
    })
  }

  put<T>(url:string,model:any,callback:(res: T)=> void){
    this._spinner.show();
    this._http.put<T>(`${this.api}/${url}`, model, { headers: this.headers }).subscribe({
      next: (res:T)=> {
        callback(res);
        this._spinner.hide();
      },
      error: (err:HttpErrorResponse)=> {
        console.log(err)
        this._toastr.error(err.error.message)
        this._spinner.hide();
      }
    })
  }

  delete<T>(url:string,model:any,callback:(res: T)=> void){
    this._spinner.show();
    this._http.delete<T>(`${this.api}/${url}`, { headers: this.headers}).subscribe({
      next: (res:T)=> {
        callback(res);
        this._spinner.hide();
      },
      error: (err:HttpErrorResponse)=> {
        console.log(err)
        this._toastr.error(err.error.message)
        this._spinner.hide();
      }
    })
  }
  
}
