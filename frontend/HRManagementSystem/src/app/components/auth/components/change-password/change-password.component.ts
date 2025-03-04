import { Component } from '@angular/core';
import { SharedModule } from '../../../../common/shared/shared/shared.module';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ChangePasswordRequestModel } from '../../models/change-password-request.model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  imports: [SharedModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent {
  
  constructor(
    private _auth: AuthService,
    private _toastr: ToastrService,
    private _router: Router
  ) { }



  changePassword(form: NgForm){
    if(form.valid){
      let model: ChangePasswordRequestModel = {
        userId: localStorage.getItem('userId'),
        currentPassword: form.value.currentpassword,
        newPassword: form.value.newpassword,
        confirmPassword: form.value.confirmpassword
      }

      console.log(model);
      this._auth.changePassword(model,res=>{
        this._toastr.success("Şifre Başarıyla Degiştirildi.")
        this._router.navigateByUrl("/");
      })

    }

  }


}
