using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Queries.GetAllEmployee;

public record GetAllEmployeeQueryResponse(ServiceResult<List<GetAllEmployeeDto>> Response);