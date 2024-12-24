namespace HrManagement.Application.Features.LeaveForm.Commands.RejectLeaveFormStatus;

public record RejectLeaveFormStatusCommandRequest(Guid Id,Guid RejectedId, string Reason)
    : ICommand<RejectLeaveFormStatusCommandResponse>;