using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Queries.GetEmployeeById;

public record GetEmployeeByIdQueryResponse(ServiceResult<GetEmployeeByIdDto> Response);