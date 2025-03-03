export class CreatePerformanceRequestModel {
  employeeId: string = "";
  workPerformanceScore: number = 0;
  teamworkScore: number = 0;
  communicationScore: number = 0;
  leadershipScore: number = 0;
  feedBack:string = "";
  reviewStartDate: Date | null = null;
}