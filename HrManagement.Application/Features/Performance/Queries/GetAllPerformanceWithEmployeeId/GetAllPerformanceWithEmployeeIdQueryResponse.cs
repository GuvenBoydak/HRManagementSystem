namespace HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;

public record GetAllPerformanceWithEmployeeIdQueryResponse(
    ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>> Result);