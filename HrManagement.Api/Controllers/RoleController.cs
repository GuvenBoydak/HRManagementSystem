using HrManagement.Application.Constant;
using HrManagement.Application.Features.AppRole.Commands.AssingRoleToUser;
using HrManagement.Application.Features.AppRole.Commands.Create;
using HrManagement.Application.Features.AppRole.Queries.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

public class RoleController(IMediator mediator) : BaseController
{
    [Authorize(Roles = $"{EmployeeConstant.HumanResources},{RoleConstant.Admin}")]
    [HttpGet]
    public async Task<IActionResult> GetAllRoles([FromRoute]GetAllRolesQueryRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = $"{EmployeeConstant.Manager},{RoleConstant.Admin}")]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = $"{EmployeeConstant.Manager},{RoleConstant.Admin}")]
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser([FromBody]AssignRoleToUserCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
}