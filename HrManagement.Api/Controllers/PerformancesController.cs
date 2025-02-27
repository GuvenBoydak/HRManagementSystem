using HrManagement.Application.Features.Performance.Commands.Create;
using HrManagement.Application.Features.Performance.Commands.Delete;
using HrManagement.Application.Features.Performance.Commands.Update;
using HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

[Authorize]
public class PerformancesController(IMediator mediator) : BaseController
{
    [HttpGet("employee/{employeeId:guid}")]
    public async Task<IActionResult> GetAllPerformanceWithEmployeeId(
        [FromRoute] Guid employeeId)
    {
        var response = await mediator.Send(new GetAllPerformanceWithEmployeeIdQueryRequest(employeeId));
        return CreateActionResult(response.Result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetPerformanceByIdQueryRequest(id));
        return CreateActionResult(response.Result);
    }
    //[Authorize(Roles = "HumanResource")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePerformanceCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = "HumanResource")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePerformanceCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = "HumanResource")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeletePerformanceCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
}