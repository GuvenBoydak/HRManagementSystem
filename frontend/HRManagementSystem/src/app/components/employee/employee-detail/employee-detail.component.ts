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
import { PayrollCreateRequestModel } from '../../payroll/models/payroll-create-request.model';
import { PayrollService } from '../../payroll/services/payroll.service';

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
    private _toastr: ToastrService,
    private _payroll: PayrollService
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
      this.closeModal();
      this.getEmployeeDetails(this.employeeId);
    })
  }

  rejectedIdBind(id: string) {
    this.rejectedLeaveformId = id;
  }

  addPayroll(form: NgForm) {
    if (form.valid) {
      let model: PayrollCreateRequestModel = {
        employeeId: this.employeeId,
        basicSalary: form.value.basicSalary,
        allowances: form.value.allowances,
        deductions: form.value.deductions,
        overtime: form.value.overtime,
        tax: form.value.tax,
        retirementFund: form.value.retirementFund,
        paymentDate: new Date(),
        bankAccountNumber: form.value.bankAccountNumber,
        comments: form.value.comments,
      }

      this._payroll.add(model,res=>{
        this._toastr.success("Maaş bilgisi eklendi.");
        this.getEmployeeDetails(this.employeeId);
        this.closeModal();
      })
    }
  }

  addPerformance(form:NgForm){
    if(form.valid){
      let model = {
        employeeId: this.employeeId,
        workPerformanceScore: form.value.workPerformanceScore,
        teamworkScore: form.value.teamworkScore,
        communicationScore: form.value.communicationScore,
        leadershipScore: form.value.leadershipScore,
        feedBack: form.value.feedBack,
        reviewStartDate: new Date()
      }

      this._performance.add(model,res=>{
        this._toastr.success("Performans bilgisi eklendi.");
        this.getEmployeeDetails(this.employeeId);
        this.closeModal();
      })
    }

  }
  
  closeModal(){
    let modal = document.getElementById('close-modal') as HTMLElement;
      if (modal) {
        modal.click();
      }
  }
}
