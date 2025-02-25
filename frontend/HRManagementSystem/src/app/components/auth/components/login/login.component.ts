import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { SharedModule } from '../../../../common/shared/shared/shared.module';
import { NgForm } from '@angular/forms';
import { LoginModel } from '../../models/login.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    private _auth: AuthService,
    private _toastr: ToastrService,
    private router: Router
  ) { }

  login(form: NgForm) {
    if (!form.valid) return;

    const { emailOrUsername, password } = form.value;
    const loginModel = new LoginModel();
    loginModel.emailOrUserName = emailOrUsername;
    loginModel.password = password;

    this._auth.login(loginModel, res => {
      this.router.navigate(['/employee']);
    });
  }
}
