using System.Net;
using AutoMapper;
using HrManagement.Application.Features.Payroll.Commands.Create;
using HrManagement.Application.Features.Payroll.Commands.Update;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;
using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Services;

public class PayrollService(IPayrollRepository payrollRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IPayrollService
{
    public async Task<ServiceResult<List<GetPayrollsWithEmployeeIdDto>>> GetPayrollsWithEmployeeIdAsync(Guid employeeId)
    {
        var payrolls = await payrollRepository.GetAsync(x => x.EmployeeId == employeeId);

        var payrollsDto = mapper.Map<List<GetPayrollsWithEmployeeIdDto>>(payrolls);

        return ServiceResult<List<GetPayrollsWithEmployeeIdDto>>.Success(payrollsDto);
    }

    public async Task<ServiceResult<GetPayrollByIdDto>> GetPayrollByIdAsync(Guid id)
    {
        var payroll = await payrollRepository.GetByIdAsync(id);
        if (payroll is null)
        {
            return ServiceResult<GetPayrollByIdDto>.Failure("", HttpStatusCode.NotFound);
        }

        var payrollDto = mapper.Map<GetPayrollByIdDto>(payroll);

        return ServiceResult<GetPayrollByIdDto>.Success(payrollDto);
    }

    public async Task<ServiceResult<Guid>> AddAsync(CreatePayrollCommandRequest request,
        CancellationToken cancellationToken)
    {
        var payroll = mapper.Map<Payroll>(request);

        await payrollRepository.AddAsync(payroll, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<Guid>.SuccessAsCreated(payroll.Id, $"api/payroll/{payroll.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(UpdatePayrollCommandRequest request)
    {
        var payroll = await payrollRepository.GetByIdAsync(request.Id);
        if (payroll is null)
        {
            return ServiceResult.Failure("", HttpStatusCode.NotFound);
        }

        var updatedPayroll = mapper.Map(request, payroll);

        payrollRepository.Update(updatedPayroll);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}