import { UserModel } from "../../auth/models/user.model";
import { EmployeeModel } from "../../employee/models/employee.model";

export class leaveFormModel{
    startDate:Date;
    endDate:Date;
    totalDays:number;
    status:number;
    reason:string;
    approvalDate:Date;
    employeeId:string;
    employee: EmployeeModel;
    approvedUser: UserModel;
}