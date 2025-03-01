import { UserModel } from "../../auth/models/user.model";
import { EmployeeModel } from "../../employee/models/employee.model";

export class PerformanceModel{
    id:string;
    feedBack:string;
    workPerformanceScore: number;
    teamworkScore: number;
    communicationScore: number;
    leadershipScore: number;
    overallScore: number;
    reviewStartDate: Date;
    reviewEndDate: Date;
    reviewedUserId: string;
    reviewedUser: UserModel;
    employeeId: string;
    employee: EmployeeModel;
}