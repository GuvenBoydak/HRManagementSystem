using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Domain.Shared;

namespace HrManagement.Application.Features.Employee.Queries.GetEmployeesBySearchWithPagination;

public record GetEmployeesBySearchWithPaginationQueryResponse(
    ServiceResult<PaginationResponse<List<GetAllEmployeeDto>>> Response);