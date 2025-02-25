import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { PayrollModel } from '../models/payroll.model';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {

  constructor(
    private _http:GenericHttpService
  ) { }

  getAll(id: string,callBack: (res: ResponseModel<PayrollModel[]>)=> void){
    this._http.get<ResponseModel<PayrollModel[]>>(`payrolls/employee/${id}`, res => {
      callBack(res);
    });
  }
}
