using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Commands.Create;

public class CreateLeaveFormCommandHandler(ILeaveFormService leaveFormService)
    : ICommandHandler<CreateLeaveFormCommandRequest, CreateLeaveFormCommandResponse>
{
    public async Task<CreateLeaveFormCommandResponse> Handle(CreateLeaveFormCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new(await leaveFormService.AddAsync(request, cancellationToken));
    }
}