using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;

public class GetAllPerformanceWithEmployeeIdQueryHandler(IPerformanceService performanceService)
    : IQueryHandler<GetAllPerformanceWithEmployeeIdQueryRequest, GetAllPerformanceWithEmployeeIdQueryResponse>
{
    public async Task<GetAllPerformanceWithEmployeeIdQueryResponse> Handle(
        GetAllPerformanceWithEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await performanceService.GetAllPerformanceWithEmployeeIdAsync(request.EmployeeId));
    }
}