using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.LeaveForm.Commands.Update;

public record UpdateLeaveFormCommandRequest(Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    int TotalDays,
    LeaveStatus Status,
    string Reason,
    Guid EmployeeId):ICommand<UpdateLeaveFormCommandResponse>;