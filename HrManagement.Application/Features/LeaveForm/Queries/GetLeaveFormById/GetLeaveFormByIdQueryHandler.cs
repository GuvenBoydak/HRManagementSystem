using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;

public class GetLeaveFormByIdQueryHandler(ILeaveFormService leaveFormService):IQueryHandler<GetLeaveFormByIdQueryRequest, GetLeaveFormByIdQueryResponse>
{
    public async Task<GetLeaveFormByIdQueryResponse> Handle(GetLeaveFormByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await leaveFormService.GetByIdAsync(request.Id));
    }
}