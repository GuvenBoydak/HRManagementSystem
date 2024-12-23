using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Api.Controllers;

public class EmployeesController(IMediator mediator):BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    { 
        var response = await mediator.Send(new GetAllEmployeeQueryRequest());
        return CreateActionResult(response.Response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await mediator.Send(new GetEmployeeByIdQueryRequest(id));
        return CreateActionResult(response.Response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
}