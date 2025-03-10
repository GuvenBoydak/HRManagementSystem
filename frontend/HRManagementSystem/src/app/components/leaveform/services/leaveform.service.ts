import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../../common/services/generic-http.service';
import { ResponseModel } from '../../../common/models/response.model';
import { leaveFormModel } from '../models/leaveform.model';
import { CreateLeaveFormRequestModel } from '../models/create-leaveform-request.model';

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
  
  add(model:CreateLeaveFormRequestModel,callBack: (res: ResponseModel<string>)=> void){
    this._http.post<ResponseModel<string>>("leaveforms",model,res=> callBack(res));
  }

  approveLeaveForm(model:any,callBack: (res: ResponseModel<string>)=> void){
    this._http.put<ResponseModel<string>>("leaveforms/approve",model,res=> callBack(res));
  }
  rejectLeaveForm(model:any,callBack: (res: ResponseModel<string>)=> void){
    this._http.put<ResponseModel<string>>("leaveforms/reject",model,res=> callBack(res));
  }
}
