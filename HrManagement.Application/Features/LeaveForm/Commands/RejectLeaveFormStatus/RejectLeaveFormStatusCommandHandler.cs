using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Commands.RejectLeaveFormStatus;

public class RejectLeaveFormStatusCommandHandler(ILeaveFormService leaveFormService)
    : ICommandHandler<RejectLeaveFormStatusCommandRequest, RejectLeaveFormStatusCommandResponse>
{
    public async Task<RejectLeaveFormStatusCommandResponse> Handle(RejectLeaveFormStatusCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new(await leaveFormService.RejectLeaveFormStatus(request));
    }
}