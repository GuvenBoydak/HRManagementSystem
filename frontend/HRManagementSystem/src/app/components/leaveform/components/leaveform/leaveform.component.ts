import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../../common/shared/shared/shared.module';
import { LeaveformService } from '../../services/leaveform.service';
import { leaveFormModel } from '../../models/leaveform.model';
import { Status } from '../../models/status.enum';

@Component({
  selector: 'app-leaveform',
  imports: [SharedModule],
  templateUrl: './leaveform.component.html',
  styleUrl: './leaveform.component.css'
})
export class LeaveformComponent implements OnInit {
  userId: string = "";
  leaveForms: leaveFormModel[] = [];
  status = Status;


constructor(
  private _leaveform: LeaveformService
){
  this.userId = localStorage.getItem("userId")
}
  ngOnInit(): void {
    this.getAllLeaveforms();
  }

getAllLeaveforms(){
  this._leaveform.getLeaveFromsWitEmployeeId(this.userId,res=>{
    this.leaveForms = res.data;
  })
}
}