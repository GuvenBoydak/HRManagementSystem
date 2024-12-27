namespace HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;

public record AssignRoleToUserCommandRequest(string RoleName,Guid UserId):ICommand<AssignRoleToUserCommandResponse>;