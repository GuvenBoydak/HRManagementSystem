import { Component, OnInit } from '@angular/core';
import { PayrollModel } from './models/payroll.model';
import { PayrollService } from './services/payroll.service';
import { SharedModule } from '../../common/shared/shared/shared.module';

@Component({
  selector: 'app-payroll',
  imports: [SharedModule],
  templateUrl: './payroll.component.html',
  styleUrl: './payroll.component.css'
})
export class PayrollComponent implements OnInit {
  userId: string = "";
  payrolls: PayrollModel[] = [];

  constructor(
    private _payroll: PayrollService
  ) {
    this.userId = localStorage.getItem("userId");
   }
  ngOnInit(): void {
    this.getAll(this.userId);
  }

  getAll(id: string) {
    this._payroll.getAll(this.userId, res=> this.payrolls = res.data);  
  }
}
