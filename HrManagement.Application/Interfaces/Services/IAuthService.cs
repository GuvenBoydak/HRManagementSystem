using HrManagement.Application.Features.AppUser.Commands.Login;
using HrManagement.Application.Features.AppUser.Commands.Register;
using HrManagement.Domain.Shared.Dtos;

namespace HrManagement.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ServiceResult<TokenDto>> LoginAsync(LoginAppUserCommandRequest request);
    Task<ServiceResult> RegisterAsync(RegisterAppUserCommandRequest request);
}