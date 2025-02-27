using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Application.Features.Employee.Queries.GetEmployeesBySearchWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
namespace HrManagement.Api.Controllers;

[Authorize]
public class EmployeesController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var response = await mediator.Send(new GetAllEmployeeQueryRequest());
        return CreateActionResult(response.Response);
    }
    
    [HttpPost("paginated")]
    public async Task<IActionResult> GetEmployeesBySearchWithPagination([FromBody] GetEmployeesBySearchWithPaginationQueryRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await mediator.Send(new GetEmployeeByIdQueryRequest(id));
        return CreateActionResult(response.Response);
    }
    [Authorize(Roles = $"{EmployeeConstant.HumanResources},{RoleConstant.Admin}")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
    [Authorize(Roles = $"{EmployeeConstant.HumanResources},{RoleConstant.Admin}")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
    [Authorize(Roles = $"{EmployeeConstant.HumanResources},{RoleConstant.Admin}")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody]DeleteEmployeeCommandRequest request)
    {
        var response = await mediator.Send(request);
        return CreateActionResult(response.Response);
    }
}