import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { EmployeeModel } from '../models/employee.model';
import { SharedModule } from '../../../common/shared/shared/shared.module';
import { Department } from '../models/departmant.enum';
import { Status } from '../../leaveform/models/status.enum';

@Component({
  selector: 'app-employee-detail',
  imports: [SharedModule],
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css'
})
export class EmployeeDetailComponent {

  employee: EmployeeModel = new EmployeeModel();
  department = Department;
  status = Status;

  constructor(
    private _employee: EmployeeService,
    private _activated: ActivatedRoute,
  ) {
    this._activated.params.subscribe(params => {
      this.getEmployeeDetails(params["id"]);
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
  
}
