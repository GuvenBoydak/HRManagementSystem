import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { PerformanceModel } from '../models/performance.model';
import { CreatePerformanceRequestModel } from '../models/create-performance-request.model';

@Injectable({
  providedIn: 'root'
})
export class PerformanceService {

  constructor(
    private _http: GenericHttpService
  ) { }

  getPerformanceByEmployeeId(id:string,callBack: (res: ResponseModel<PerformanceModel[]>)=> void){
     this._http.get<ResponseModel<PerformanceModel[]>>(`performances/employee/${id}`,res=> callBack(res));
  }

  add(model: CreatePerformanceRequestModel, callBack: (res: ResponseModel<string>)=> void){
    this._http.post<ResponseModel<string>>("performances",model,res=> callBack(res));
  }

  update(model: PerformanceModel, callBack: (res: ResponseModel<string>) => void) {
    this._http.put<ResponseModel<string>>(`performances`, model, res => callBack(res));
  }
}
