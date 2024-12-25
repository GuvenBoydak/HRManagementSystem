using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
    : IQueryHandler<GetEmployeeByIdQueryRequest, GetEmployeeByIdQueryResponse>
{
    public async Task<GetEmployeeByIdQueryResponse> Handle(GetEmployeeByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        return new(await employeeService.GetByIdAsync(request.Id));
    }
}