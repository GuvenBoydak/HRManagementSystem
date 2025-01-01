using HrManagement.Application.Features.Employee.Dtos;

namespace HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;

public record GetAllPerformanceWithEmployeeIdDto(
    Guid Id,
    string FeedBack,
    int WorkPerformanceScore,
    int TeamworkScore,
    int CommunicationScore,
    int LeadershipScore,
    int OverallScore,
    DateTime ReviewStartDate,
    DateTime ReviewEndDate,
    Guid ReviewedUserId,
    Guid EmployeeId,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    DateTime DeletedDate,
    bool IsDeleted,
    EmployeeDto Employee);