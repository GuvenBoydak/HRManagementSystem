using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.AppUser.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAuthService authService):ICommandHandler<ChangePasswordCommandRequest,ChangePasswordCommandResponse>
{
    public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await authService.ChangePasswordAsync(request));
    }
}