namespace HrManagement.Application.Features.AppUser.Commands.Register;

public record RegisterAppUserCommandRequest(string FirstName,
    string Surname,
    string UserName,
    string Email,
    string Password)
    : ICommand<RegisterAppUserCommandResponse>; 