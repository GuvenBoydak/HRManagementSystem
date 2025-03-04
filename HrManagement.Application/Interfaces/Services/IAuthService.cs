using HrManagement.Application.Features.AppUser.Commands.ChangePassword;
using HrManagement.Application.Features.AppUser.Commands.Login;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Shared.Dtos;

namespace HrManagement.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ServiceResult<AppUser>> GetByIdAsync(Guid id);
    Task<ServiceResult<TokenDto>> LoginAsync(LoginAppUserCommandRequest request);
    Task<ServiceResult> RegisterAsync(AppUser user,string password);
    Task<ServiceResult> ChangePasswordAsync(ChangePasswordCommandRequest request);
}