import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { EmployeeModel } from '../models/employee.model';
import { SharedModule } from '../../../common/shared/shared/shared.module';
import { Department } from '../models/departmant.enum';
import { Status } from '../../leaveform/models/status.enum';
import { PerformanceService } from '../../performance/services/performance.service';
import { PerformanceModel } from '../../performance/models/performance.model';
import { ToastrService } from 'ngx-toastr';
import { LeaveformService } from '../../leaveform/services/leaveform.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-employee-detail',
  imports: [SharedModule],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css'
})
export class EmployeeDetailComponent {
  employeeId: string = "";
  employee: EmployeeModel = new EmployeeModel();
  department = Department;
  status = Status;
  rejectedLeaveformId: string = "";

  constructor(
    private _employee: EmployeeService,
    private _activated: ActivatedRoute,
    private _performance: PerformanceService,
    private _leaveform: LeaveformService,
    private _toastr: ToastrService
  ) {
    this._activated.params.subscribe(params => {
      this.employeeId = params["id"];
      this.getEmployeeDetails(this.employeeId);
    })
  }

  getEmployeeDetails(id: string) {
    this._employee.getById(id, res => {
      this.employee = res.data;
    });
  }

  // Performans puanlarını yıldız sayısına çeviren fonksiyon
  getStars(score: number): number[] {
    const fullStars = Math.round(score / 2);
    return new Array(fullStars).fill(0);
  }
  getPerformanceRating(score: number): string {
    if (score >= 8) {
      return 'status-good';
    } else if (score >= 5) {
      return 'status-medium';
    } else {
      return 'status-bad';
    }
  }

  isDefaultReviewEndDate(date: Date | string | null | undefined): boolean {
    if (!date) return true;

    const parsedDate = new Date(date);
    return parsedDate.getFullYear() === 1;
  }

  updatePerformance(model: PerformanceModel) {
    model.reviewEndDate = new Date();
    model.reviewedUserId = localStorage.getItem("userId");

    this._performance.update(model, res => {
      this._toastr.success("Degerlendirme bitirildi.");
      this.getEmployeeDetails(this.employeeId);
    })
  }

  approveLeaveForm(id: string) {
    const model = {
      id: id,
      approvedId: localStorage.getItem("userId")
    };

    this._leaveform.approveLeaveForm(model, res => {
      this._toastr.success("İzin talebi onaylandı.");
      this.getEmployeeDetails(this.employeeId);
    })
  }

  rejectLeaveForm(form: NgForm) {
    const model = {
      id: this.rejectedLeaveformId,
      rejectedId: localStorage.getItem("userId"),
      reason: form.value.reason
    };

    this._leaveform.rejectLeaveForm(model, res => {
      this._toastr.success("İzin talebi Reddedildi.");
      let modal = document.getElementById('close-modal') as HTMLElement;
      if (modal) {
        modal.click();
      }
      this.getEmployeeDetails(this.employeeId);
    })
  }

  rejectedIdBind(id: string) {
    this.rejectedLeaveformId = id;
  }
}
