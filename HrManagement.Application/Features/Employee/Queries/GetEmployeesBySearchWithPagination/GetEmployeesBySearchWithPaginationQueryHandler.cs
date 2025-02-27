using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Queries.GetEmployeesBySearchWithPagination;

public class GetEmployeesBySearchWithPaginationQueryHandler(IEmployeeService employeeService):IQueryHandler<GetEmployeesBySearchWithPaginationQueryRequest,GetEmployeesBySearchWithPaginationQueryResponse>
{
    public async Task<GetEmployeesBySearchWithPaginationQueryResponse> Handle(GetEmployeesBySearchWithPaginationQueryRequest request, CancellationToken cancellationToken)
    {
        return new GetEmployeesBySearchWithPaginationQueryResponse(await employeeService.GetEmployeesBySearchWithPaginationAsync(request.Search,request.PageNumber, request.PageSize,cancellationToken));
    }
}