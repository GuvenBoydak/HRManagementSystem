namespace HrManagement.Application.Features.Employee.Queries.GetEmployeeById;

public record GetEmployeeByIdQueryRequest(Guid Id) : IQuery<GetEmployeeByIdQueryResponse>;