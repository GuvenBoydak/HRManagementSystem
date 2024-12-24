using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Commands.ApproveLeaveFormStatus;

public class ApproveLeaveFormStatusCommandHandler(ILeaveFormService leaveFormService):ICommandHandler<ApproveLeaveFromStatusCommandRequest,ApproveLeaveFormStatusCommandResponse>
{
    public async Task<ApproveLeaveFormStatusCommandResponse> Handle(ApproveLeaveFromStatusCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await leaveFormService.ApproveLeaveFormStatus(request));
    }
}