
export class PayrollCreateRequestModel{
    basicSalary: number;
    allowances: number;
    overtime: number;
    deductions: number;
    tax: number;
    paymentDate: Date;
    bankAccountNumber: string;
    comments: string;
    retirementFund: number;
    employeeId: string;;
}