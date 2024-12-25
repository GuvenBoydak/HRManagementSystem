using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Performance.Commands.Create;

public class CreatePerformanceCommandHandler(IPerformanceService performanceService)
    : ICommandHandler<CreatePerformanceCommandRequest, CreatePerformanceCommandResponse>
{
    public async Task<CreatePerformanceCommandResponse> Handle(CreatePerformanceCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new(await performanceService.AddAsync(request, cancellationToken));
    }
}