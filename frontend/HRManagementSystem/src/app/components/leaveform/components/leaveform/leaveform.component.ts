import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../../common/shared/shared/shared.module';
import { LeaveformService } from '../../services/leaveform.service';
import { leaveFormModel } from '../../models/leaveform.model';
import { Status } from '../../models/status.enum';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CreateLeaveFormRequestModel } from '../../models/create-leaveform-request.model';

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
  totalDays: number = 0;


  constructor(
    private _leaveform: LeaveformService,
    private _toastr: ToastrService
  ) {
    this.userId = localStorage.getItem("userId")
  }
  ngOnInit(): void {
    this.getAllLeaveforms();
  }

  getAllLeaveforms() {
    this._leaveform.getLeaveFromsWitEmployeeId(this.userId, res => {
      this.leaveForms = res.data.sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime());
    })
  }
  add(form: NgForm) {
    if (form.valid) {
      let model: CreateLeaveFormRequestModel = {
        employeeId: this.userId,
        startDate: form.value.startDate,
        endDate: form.value.endDate,
        totalDays: this.totalDays,
        reason: form.value.reason
      };

      this._leaveform.add(model, res => {
        this._toastr.success("İzin Formu Eklendi");
        this.getAllLeaveforms();
        let modal = document.getElementById('close-modal') as HTMLElement;
        if (modal) {
          modal.click();
        }
      })
    }
  }

  calculateDays(startDate: string, endDate: string): void {
    if (startDate !== endDate) {
      console.log(startDate);
      const start = new Date(startDate);
      const end = new Date(endDate);

      const timeDifference = end.getTime() - start.getTime();
      this.totalDays = timeDifference / (1000 * 3600 * 24) + 1;
    }
  }
}