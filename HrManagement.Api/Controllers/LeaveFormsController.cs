using HrManagement.Application.Features.LeaveForm.Commands.Create;
using HrManagement.Application.Features.LeaveForm.Commands.Update;
using HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

public class LeaveFormsController(IMediator mediator):BaseController
{
    [HttpGet("employee/{employeeId:guid}")]
    public async Task<IActionResult> GetAllWithEmployeeId([FromRoute]Guid employeeId)
    {
        var response = await mediator.Send(new GetAllWithEmployeeIdQueryRequest(employeeId));
        return CreateActionResult(response.Response);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var response = await mediator.Send(new GetLeaveFormByIdQueryRequest(id));
        return CreateActionResult(response.Response);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveFormCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLeaveFormCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
}