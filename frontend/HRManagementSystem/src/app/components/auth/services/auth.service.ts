import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { LoginModel } from '../components/login/models/login.model';
import { ResponseModel } from '../../../common/models/response.model';
import { TokenModel } from '../components/login/models/token.model';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private _http:GenericHttpService
  ) { }

  login(model:LoginModel,callBack: (res: ResponseModel<TokenModel>) => void){
    this._http.post<ResponseModel<TokenModel>>('auths/login',model,res=> {
      localStorage.setItem('token', res.data.accessToken);
      localStorage.setItem('role', this.getUserRole());
      localStorage.setItem('userId', this.getUserId());
      callBack(res)
    });
  }

  getUserRole(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken = jwtDecode(token) as Record<string, any>; // Tip güvenli hale getiriyoruz
        const roleKey = Object.keys(decodedToken).find(key => key.includes('role')); // Dinamik olarak role key'ini bul
        return roleKey ? decodedToken[roleKey] : null;
      } catch (error) {
        console.error('Token çözümleme hatası:', error);
        return null;
      }
    }
    return null;
  }
  getUserId(): string | null {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        return decodedToken.sub;
      } catch (error) {
        console.error('Token çözümleme hatası:', error);
        return null;
      }
    }
    return null;
  }
}
