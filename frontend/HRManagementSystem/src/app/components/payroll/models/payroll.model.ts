import { EmployeeModel } from "../../employee/models/employee.model";

export class PayrollModel{
    basicSalary: number;
    allowances: number;
    overtime: number;
    deductions: number;
    tax: number;
    paymentDate: Date;
    bankAccountNumber: string;
    comments: string;
    retirementFund: number;
    grossSalary: number;
    netSalary: number;
    employeeId: string;
    employee: EmployeeModel;
}