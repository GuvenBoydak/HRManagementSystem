using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;

public record GetLeaveFormByIdDto(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    int TotalDays,
    LeaveStatus Status,
    string Reason,
    Guid EmployeeId,
    Guid ApprovedUserId,
    DateTime ApprovalDate,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    DateTime DeletedDate,
    bool IsDeleted);