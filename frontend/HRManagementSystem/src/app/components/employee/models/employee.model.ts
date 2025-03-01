import { leaveFormModel } from "../../leaveform/models/leaveform.model";
import { PayrollModel } from "../../payroll/models/payroll.model";
import { PerformanceModel } from "../../performance/models/performance.model";

export class EmployeeModel {
    id: number;
    name: string;
    surname: string;
    email: string;
    phone: string;
    gender: number;
    dateOfBirth: Date;
    address: string;
    position: string;
    department: number;
    hireDate: Date;
    salary: number;
    performanceScore: number;
    leaveUsed: number;
    createdDate: Date;
    payrolls: PayrollModel[];
    performances: PerformanceModel[];
    leaveForms: leaveFormModel[];
}