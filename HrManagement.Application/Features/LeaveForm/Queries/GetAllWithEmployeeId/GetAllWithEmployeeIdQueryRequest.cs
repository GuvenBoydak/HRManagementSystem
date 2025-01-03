namespace HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;

public record GetAllWithEmployeeIdQueryRequest(Guid EmployeeId) : IQuery<GetAllWithEmployeeIdQueryResponse>;