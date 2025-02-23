import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { LoginModel } from '../components/login/models/login.model';
import { ResponseModel } from '../../../common/models/response.model';
import { TokenModel } from '../components/login/models/token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private _http:GenericHttpService
  ) { }

  login(model:LoginModel,callBack: (res: ResponseModel<TokenModel>) => void){
    this._http.post<ResponseModel<TokenModel>>('auths/login',model,res=> callBack(res));
  }
}
