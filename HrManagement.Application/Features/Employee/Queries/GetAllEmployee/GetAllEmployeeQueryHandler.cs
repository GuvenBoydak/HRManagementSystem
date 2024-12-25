using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Queries.GetAllEmployee;

public class GetAllEmployeeQueryHandler(IEmployeeService employeeService)
    : IQueryHandler<GetAllEmployeeQueryRequest, GetAllEmployeeQueryResponse>
{
    public async Task<GetAllEmployeeQueryResponse> Handle(GetAllEmployeeQueryRequest request,
        CancellationToken cancellationToken)
    {
        return new(await employeeService.GetAllAsync());
    }
}