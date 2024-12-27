namespace HrManagement.Application.Features.AppUser.Commands.Login;

public record LoginAppUserCommandRequest(string EmailOrUserName,
    string Password):ICommand<LoginAppUserCommandResponse>;