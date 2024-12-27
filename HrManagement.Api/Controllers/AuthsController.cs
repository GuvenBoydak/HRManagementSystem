using HrManagement.Application.Features.AppUser.Commands.Login;
using HrManagement.Application.Features.AppUser.Commands.Register;
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
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAppUserCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
}