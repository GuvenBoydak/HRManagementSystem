using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Payroll.Queries.GetPayrollById;

public class GetPayrollByIdQueryHandler(IPayrollService payrollService):IQueryHandler<GetPayrollByIdQueryRequest, GetPayrollByIdQueryResponse>
{
    public async Task<GetPayrollByIdQueryResponse> Handle(GetPayrollByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return new(await payrollService.GetPayrollByIdAsync(request.Id));
    }
}
