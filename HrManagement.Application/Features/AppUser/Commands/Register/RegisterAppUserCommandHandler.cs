using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppUser.Commands.Register;

public class RegisterAppUserCommandHandler(IAuthService authService):ICommandHandler<RegisterAppUserCommandRequest,RegisterAppUserCommandResponse>
{
    public async Task<RegisterAppUserCommandResponse> Handle(RegisterAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await authService.RegisterAsync(request));
    }
}