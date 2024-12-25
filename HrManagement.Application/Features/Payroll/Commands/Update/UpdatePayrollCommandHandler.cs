using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Payroll.Commands.Update;

public class UpdatePayrollCommandHandler(IPayrollService payrollService):ICommandHandler<UpdatePayrollCommandRequest, UpdatePayrollCommandResponse>
{
    public async Task<UpdatePayrollCommandResponse> Handle(UpdatePayrollCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await payrollService.UpdateAsync(request));
    }
}