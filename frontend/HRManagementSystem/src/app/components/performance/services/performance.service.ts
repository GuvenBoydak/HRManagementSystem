import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { PerformanceModel } from '../models/performance.model';

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
}
