import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../common/shared/shared/shared.module';
import { TableComponent } from '../../common/components/table/table.component';
import { Router } from '@angular/router';
import { EmployeeModel } from './models/employee.model';
import { EmployeeService } from './services/employee.service';
import { Department } from './models/departmant.enum';

@Component({
  selector: 'app-employee',
  imports: [SharedModule,TableComponent],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {
search: string;
employees: EmployeeModel[] = [];
departmentEnum = Department;

constructor(
  private _employee: EmployeeService,
  private router: Router
) {}
  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this._employee.getAll(res=> this.employees = res.data);
  }

detailEmployee(employee:EmployeeModel){
}
}
