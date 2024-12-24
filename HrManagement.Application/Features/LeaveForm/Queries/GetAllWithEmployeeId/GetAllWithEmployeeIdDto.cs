using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;

public record GetAllWithEmployeeIdDto(Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    int TotalDays,
    LeaveStatus Status,
    string Reason,
    Guid EmployeeId,
    Guid ApprovedUserId,
    Guid ApprovalDate,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    DateTime DeletedDate,
    bool IsDeleted);