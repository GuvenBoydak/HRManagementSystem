using HrManagement.Application.Features.Payroll.Commands.Create;
using HrManagement.Application.Features.Payroll.Commands.Update;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

[Authorize]
public class PayrollsController(IMediator mediator):BaseController
{
    [HttpGet("employee/{employeeId:guid}")]
    public async Task<IActionResult> GetPayrollsWithEmployeeId([FromRoute] Guid employeeId)
    {
        var response = await mediator.Send(new GetPayrollsWithEmployeeIdQueryRequest(employeeId));
        return CreateActionResult(response.Result);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPayrollById([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetPayrollByIdQueryRequest(id));
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = "HumanResource")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePayrollCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
    [Authorize(Roles = "HumanResource")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePayrollCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Result);
    }
}
