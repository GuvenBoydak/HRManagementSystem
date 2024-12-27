using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Shared.Dtos;

namespace HrManagement.Application.Interfaces.Services;

public interface IJwtProviderService
{
    Task<TokenDto> CreateTokenAsync(AppUser user);
}