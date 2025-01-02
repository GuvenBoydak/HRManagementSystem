using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Commands.AproveLeaveFormStatus;

public class AproveLeaveFormStatusCommandHandler(ILeaveFormService leaveFormService)
    : ICommandHandler<AproveLeaveFromStatusCommandRequest, AproveLeaveFormStatusCommandResponse>
{
    public async Task<AproveLeaveFormStatusCommandResponse> Handle(AproveLeaveFromStatusCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new(await leaveFormService.ApproveLeaveFormStatus(request));
    }
}