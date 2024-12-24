using System.Net;
using AutoMapper;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.LeaveForm.Commands.ApproveLeaveFormStatus;
using HrManagement.Application.Features.LeaveForm.Commands.Create;
using HrManagement.Application.Features.LeaveForm.Commands.RejectLeaveFormStatus;
using HrManagement.Application.Features.LeaveForm.Commands.Update;
using HrManagement.Application.Features.LeaveForm.Queries.GetAllWithEmployeeId;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;
using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities;
using HrManagement.Domain.Enums;

namespace HrManagement.Application.Services;

public class LeaveFormService(ILeaveFormRepository leaveFormRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : ILeaveFormService
{
    public async Task<ServiceResult<List<GetAllWithEmployeeIdDto>>> GetAllWithEmployeeIdAsync(Guid employeeId)
    {
        var leaveForms = await leaveFormRepository.GetAsync(x => x.EmployeeId == employeeId, false);

        var leaveFormsDto = mapper.Map<List<GetAllWithEmployeeIdDto>>(leaveForms);

        return ServiceResult<List<GetAllWithEmployeeIdDto>>.Succes(leaveFormsDto);
    }

    public async Task<ServiceResult<GetLeaveFormByIdDto>> GetByIdAsync(Guid id)
    {
        var leaveForm = await leaveFormRepository.GetByIdAsync(id);
        if (leaveForm is null)
        {
            return ServiceResult<GetLeaveFormByIdDto>.Failure(LeaveFormConstant.NotFound, HttpStatusCode.NotFound);
        }

        var leaveFormDto = mapper.Map<GetLeaveFormByIdDto>(leaveForm);

        return ServiceResult<GetLeaveFormByIdDto>.Succes(leaveFormDto);
    }

    public async Task<ServiceResult<Guid>> AddAsync(CreateLeaveFormCommandRequest request,
        CancellationToken cancellationToken)
    {
        var leaveForm = mapper.Map<LeaveForm>(request);

        await leaveFormRepository.AddAsync(leaveForm, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<Guid>.SuccesAsCreated(leaveForm.Id, $"api/LeaveForm/{leaveForm.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(UpdateLeaveFormCommandRequest request)
    {
        var leaveForm = await leaveFormRepository.GetByIdAsync(request.Id);
        if (leaveForm is null)
        {
            return ServiceResult.Failure(LeaveFormConstant.NotFound, HttpStatusCode.NotFound);
        }

        var updatedLeaveForm = mapper.Map(request, leaveForm);

        leaveFormRepository.Update(updatedLeaveForm);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Succes(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> ApproveLeaveFormStatus(ApproveLeaveFromStatusCommandRequest request)
    {
        return await UpdateLeaveFormStatus(request.Id, LeaveStatus.Approved, request.ApprovedId, "");
    }

    public async Task<ServiceResult> RejectLeaveFormStatus(RejectLeaveFormStatusCommandRequest request)
    {
        return await UpdateLeaveFormStatus(request.Id, LeaveStatus.Rejected, request.RejectedId, request.Reason);
    }

    private async Task<ServiceResult> UpdateLeaveFormStatus(Guid id, LeaveStatus status, Guid userId, string reason)
    {
        var leaveForm = await leaveFormRepository.GetByIdAsync(id);
        if (leaveForm is null)
        {
            return ServiceResult.Failure(LeaveFormConstant.NotFound, HttpStatusCode.NotFound);
        }

        leaveForm.Status = status;
        leaveForm.ApprovedUserId = userId;
        leaveForm.ApprovalDate = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(reason))
        {
            leaveForm.Reason = reason;
        }

        leaveFormRepository.Update(leaveForm);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Succes(HttpStatusCode.NoContent);
    }
}