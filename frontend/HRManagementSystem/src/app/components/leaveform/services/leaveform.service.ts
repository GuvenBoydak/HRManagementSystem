import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { leaveFormModel } from '../models/leaveform.model';

@Injectable({
  providedIn: 'root'
})
export class LeaveformService {

  constructor(
    private _http: GenericHttpService
  ) { }

  getLeaveFromsWitEmployeeId(id:string,callBack: (res: ResponseModel<leaveFormModel[]>)=> void){
    this._http.get<ResponseModel<leaveFormModel[]>>(`leaveforms/employee/${id}`,res=> callBack(res));
  }
}
