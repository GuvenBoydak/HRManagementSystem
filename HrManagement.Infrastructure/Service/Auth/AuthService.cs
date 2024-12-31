using System.Net;
using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.AppUser.Commands.Login;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace HrManagement.Infrastructure.Service.Auth;

public class AuthService(UserManager<AppUser> userManager,IJwtProviderService jwtProviderService):IAuthService
{
    public async Task<ServiceResult<AppUser>> GetByIdAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return ServiceResult<AppUser>.Failure(UserConstant.NotFound, HttpStatusCode.NotFound);
        }

        return ServiceResult<AppUser>.Success(user);
    }

    public async Task<ServiceResult<TokenDto>> LoginAsync(LoginAppUserCommandRequest request)
    {
        var checkUser = userManager.Users.FirstOrDefault(x =>
            x.Email == request.EmailOrUserName || x.UserName == request.EmailOrUserName);
        if (checkUser is null)
        {
            return ServiceResult<TokenDto>.Failure(UserConstant.NotFound,HttpStatusCode.NotFound);
        }
        
        var checkPassword = await userManager.CheckPasswordAsync(checkUser, request.Password);
        if (!checkPassword)
        {
            return ServiceResult<TokenDto>.Failure(UserConstant.InvalidPassword,HttpStatusCode.BadRequest);
        }

        return ServiceResult<TokenDto>.Success(await jwtProviderService.CreateTokenAsync(checkUser));
    }

    public async Task<ServiceResult> RegisterAsync(AppUser user,string password)
    {
        var checkUser = userManager.Users.FirstOrDefault(x =>
            x.Email == user.Email || x.UserName == user.UserName);
        if (checkUser is not null)
        {
            return ServiceResult.Failure(UserConstant.ExistUser,HttpStatusCode.BadRequest);
        }
        
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return ServiceResult.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}