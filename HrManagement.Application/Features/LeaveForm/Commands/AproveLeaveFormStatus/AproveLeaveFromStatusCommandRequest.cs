namespace HrManagement.Application.Features.LeaveForm.Commands.AproveLeaveFormStatus;

public record AproveLeaveFromStatusCommandRequest(Guid Id, Guid ApprovedId)
    : ICommand<AproveLeaveFormStatusCommandResponse>;