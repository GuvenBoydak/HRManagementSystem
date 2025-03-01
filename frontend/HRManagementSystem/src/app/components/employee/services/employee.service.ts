import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { EmployeeModel } from '../models/employee.model';
import { PaginationModel } from '../../../common/models/pagination.model';
import { RequestEmployeeModel } from '../models/request-employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(
    private _http: GenericHttpService
  ) { }

  getAllWithPaginaion(model:RequestEmployeeModel,callBack: (res: ResponseModel<PaginationModel<EmployeeModel[]>>) => void) {
    this._http.post<ResponseModel<PaginationModel<EmployeeModel[]>>>(`employees/paginated`,model, res => {
      callBack(res);
    });
  }
  
  getById(id:string,callBack:(res: ResponseModel<EmployeeModel>)=> void){
    this._http.get<ResponseModel<EmployeeModel>>(`employees/${id}`,res=> callBack(res));
  }

  add(model: EmployeeModel, callBack: (res: ResponseModel<string>) => void) {
    this._http.post<ResponseModel<string>>("employees", model, res => callBack(res));
  }
}
