using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;

public class GetPayrollsWithEmployeeIdQueryHandler(IPayrollService payrollService):IQueryHandler<GetPayrollsWithEmployeeIdQueryRequest,GetPayrollsWithEmployeeIdQueryResponse>
{
    public async Task<GetPayrollsWithEmployeeIdQueryResponse> Handle(GetPayrollsWithEmployeeIdQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await payrollService.GetPayrollsWithEmployeeIdAsync(request.EmployeeId));
    }
}