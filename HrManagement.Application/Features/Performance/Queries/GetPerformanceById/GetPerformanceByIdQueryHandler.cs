using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Performance.Queries.GetPerformanceById;

public class GetPerformanceByIdQueryHandler(IPerformanceService performanceService)
    : IQueryHandler<GetPerformanceByIdQueryRequest, GetPerformanceByIdQueryResponse>
{
    public async Task<GetPerformanceByIdQueryResponse> Handle(GetPerformanceByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        return new(await performanceService.GetPerformanceByIdAsync(request.Id));
    }
}