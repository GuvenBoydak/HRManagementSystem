namespace HrManagement.Application.Features.LeaveForm.Commands.ApproveLeaveFormStatus;

public record ApproveLeaveFromStatusCommandRequest(Guid Id,Guid ApprovedId) : ICommand<ApproveLeaveFormStatusCommandResponse>;