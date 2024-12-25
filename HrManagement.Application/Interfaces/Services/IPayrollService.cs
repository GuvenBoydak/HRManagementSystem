using HrManagement.Application.Features.Payroll.Commands.Create;
using HrManagement.Application.Features.Payroll.Commands.Update;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;

namespace HrManagement.Application.Interfaces.Services;

public interface IPayrollService
{
    Task<ServiceResult<List<GetPayrollsWithEmployeeIdDto>>> GetPayrollsWithEmployeeIdAsync(Guid employeeId);
    Task<ServiceResult<GetPayrollByIdDto>> GetPayrollByIdAsync(Guid id);
    Task<ServiceResult<Guid>> AddAsync(CreatePayrollCommandRequest request,CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(UpdatePayrollCommandRequest request);
}