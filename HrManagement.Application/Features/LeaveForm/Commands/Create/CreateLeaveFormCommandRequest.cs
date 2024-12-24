using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.LeaveForm.Commands.Create;

public record CreateLeaveFormCommandRequest(
    DateTime StartDate,
    DateTime EndDate,
    int TotalDays,
    LeaveStatus Status,
    string Reason,
    Guid EmployeeId) : ICommand<CreateLeaveFormCommandResponse>;