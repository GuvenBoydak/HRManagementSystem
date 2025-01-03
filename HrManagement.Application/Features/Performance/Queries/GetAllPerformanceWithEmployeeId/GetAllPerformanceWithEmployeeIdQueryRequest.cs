namespace HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;

public record GetAllPerformanceWithEmployeeIdQueryRequest(Guid EmployeeId)
    : IQuery<GetAllPerformanceWithEmployeeIdQueryResponse>;