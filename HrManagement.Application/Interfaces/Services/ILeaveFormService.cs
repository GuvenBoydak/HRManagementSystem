using HrManagement.Application.Features.LeaveForm.Commands.Create;
using HrManagement.Application.Features.LeaveForm.Commands.Update;
using HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;

namespace HrManagement.Application.Interfaces.Services;

public interface ILeaveFormService
{
    Task<ServiceResult<List<GetAllWithEmployeeIdDto>>> GetAllWithEmployeeIdAsync(Guid employeeId);
    Task<ServiceResult<GetLeaveFormByIdDto>> GetByIdAsync(Guid id);
    Task<ServiceResult<Guid>> AddAsync(CreateLeaveFormCommandRequest request, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(UpdateLeaveFormCommandRequest request);
}