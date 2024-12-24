using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Commands.Update;

public class UpdateLeaveFormCommandHandler(ILeaveFormService leaveFormService):ICommandHandler<UpdateLeaveFormCommandRequest,UpdateLeaveFormCommandResponse>
{
    public async Task<UpdateLeaveFormCommandResponse> Handle(UpdateLeaveFormCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await leaveFormService.UpdateAsync(request));
    }
}