using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Performance.Commands.Update;

public class UpdatePerformanceCommandHandler(IPerformanceService performanceService)
    : ICommandHandler<UpdatePerformanceCommandRequest, UpdatePerformanceCommandResponse>
{
    public async Task<UpdatePerformanceCommandResponse> Handle(UpdatePerformanceCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new(await performanceService.UpdateAsync(request));
    }
}