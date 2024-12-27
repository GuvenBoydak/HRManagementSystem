namespace HrManagement.Application.Features.AppRole.Commands.Create;

public record CreateRoleCommandRequest(string Name):ICommand<CreateRoleCommandResponse>;