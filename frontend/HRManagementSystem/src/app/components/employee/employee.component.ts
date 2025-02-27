import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../common/shared/shared/shared.module';
import { TableComponent } from '../../common/components/table/table.component';
import { Router } from '@angular/router';
import { EmployeeModel } from './models/employee.model';
import { EmployeeService } from './services/employee.service';
import { Department } from './models/departmant.enum';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PaginationModel } from '../../common/models/pagination.model';

@Component({
  selector: 'app-employee',
  imports: [SharedModule, TableComponent],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {
  role: string;
  search: string = "";
  employees: PaginationModel<EmployeeModel[]> = new PaginationModel<EmployeeModel[]>;
  pageNumber: number[] = [];
  departmentEnum = Department;


  constructor(
    private _employee: EmployeeService,
    private _toastr: ToastrService,
    private router: Router
  ) {
    this.role = localStorage.getItem('role');
   }
  ngOnInit(): void {
    this.getAllWithPaginaion();
  }

  getAllWithPaginaion(pageNumber: number = 1) {
    this.employees.pageNumber = pageNumber;
    let requestEmployee = { search: this.search,
       pageNumber: this.employees.pageNumber,
        pageSize: this.employees.pageSize };

    this._employee.getAllWithPaginaion(requestEmployee, res => {
      this.employees = res.data;
      this.setPageNumbers();
    });
  }

  setPageNumbers(): void {
    const totalPages = Math.ceil(this.employees.totalCount / this.employees.pageSize);
    const currentPage = this.employees.pageNumber;
  
    let startPage = Math.max(1, currentPage - 2);
    let endPage = Math.min(totalPages, currentPage + 2);
  
    if (currentPage === totalPages) {
      startPage = Math.max(1, totalPages - 4);
      endPage = totalPages;
    }
  
    this.pageNumber = [];
    for (let i = startPage; i <= endPage; i++) {
      this.pageNumber.push(i);
    }
  }
    

  detailEmployee(employee: EmployeeModel) {
  }

  add(form: NgForm) {
    let model: EmployeeModel = {
      ...form.value,
      gender: +form.value.gender,
      department: +form.value.department
    };

    if (form.valid) {
      this._employee.add(model, res => {
        this._toastr.success("Kayıt Başarılı");
        this.getAllWithPaginaion();
        let modal = document.getElementById('close-modal') as HTMLElement;
        if (modal) {
          modal.click();
        }
      })
    }
  }
}
