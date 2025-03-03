import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { PayrollModel } from '../models/payroll.model';
import { PayrollCreateRequestModel } from '../models/payroll-create-request.model';

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

  add(modle:PayrollCreateRequestModel,callBack:(res: ResponseModel<string>)=> void){
    this._http.post<ResponseModel<string>>('payrolls',modle,res => {
      callBack(res);
    });
  }
}
