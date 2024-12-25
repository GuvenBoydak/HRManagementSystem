namespace HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;

public record GetLeaveFormByIdQueryRequest(Guid Id) : IQuery<GetLeaveFormByIdQueryResponse>;