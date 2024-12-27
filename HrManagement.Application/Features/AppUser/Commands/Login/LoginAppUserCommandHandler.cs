using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppUser.Commands.Login;

public class LoginAppUserCommandHandler(IAuthService authService):ICommandHandler<LoginAppUserCommandRequest,LoginAppUserCommandResponse>
{
    public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await authService.LoginAsync(request));
    }
}