import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { EmployeeModel } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private _http: GenericHttpService
  ) { }

  getAll(callBack: (res: ResponseModel<EmployeeModel[]>)=> void){
    this._http.get<ResponseModel<EmployeeModel[]>>("employees",res=> {
      callBack(res);
    });
  }
}
