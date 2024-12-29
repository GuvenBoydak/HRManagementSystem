using System.Net;
using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.AppUser.Commands.Login;
using HrManagement.Application.Features.AppUser.Commands.Register;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace HrManagement.Infrastructure.Service.Auth;

public class AuthService(UserManager<AppUser> userManager,IJwtProviderService jwtProviderService):IAuthService
{
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

    public async Task<ServiceResult> RegisterAsync(RegisterAppUserCommandRequest request)
    {
        var checkUser = userManager.Users.FirstOrDefault(x =>
            x.Email == request.Email || x.UserName == request.UserName);
        if (checkUser is not null)
        {
            return ServiceResult.Failure(UserConstant.ExistUser,HttpStatusCode.BadRequest);
        }
        
        var user = new AppUser
        {
            NameSurname = request.FirstName + request.Surname,
            UserName = request.UserName,
            Email = request.Email
        };
        
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return ServiceResult.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}