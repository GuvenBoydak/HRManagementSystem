using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;

public class GetAllWithEmployeeIdQueryHandler(ILeaveFormService leaveFormService):IQueryHandler<GetAllWithEmployeeIdQueryRequest,GetAllWithEmployeeIdQueryResponse>
{
    public async Task<GetAllWithEmployeeIdQueryResponse> Handle(GetAllWithEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await leaveFormService.GetAllWithEmployeeIdAsync(request.EmployeeId));
    }
}