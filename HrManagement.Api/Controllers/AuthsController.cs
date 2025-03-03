using HrManagement.Application.Features.AppUser.Commands.ChangePassword;
using HrManagement.Application.Features.AppUser.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

public class AuthsController(IMediator mediator):BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAppUserCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    
    [HttpPost("change-password")]
    public async Task<IActionResult> Login(ChangePasswordCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
}