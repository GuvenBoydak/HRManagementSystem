using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Performance.Commands.Delete;

public class DeletePerformanceCommandHandler(IPerformanceService performanceService)
    : ICommandHandler<DeletePerformanceCommandRequest, DeletePerformanceCommandResponse>
{
    public async Task<DeletePerformanceCommandResponse> Handle(DeletePerformanceCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await performanceService.DeleteAsync(request));
    }
}