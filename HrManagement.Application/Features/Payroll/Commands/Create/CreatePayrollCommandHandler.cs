using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Payroll.Commands.Create;

public class CreatePayrollCommandHandler(IPayrollService payrollService):ICommandHandler<CreatePayrollCommandRequest,CreatePayrollCommandResponse>
{
    public async Task<CreatePayrollCommandResponse> Handle(CreatePayrollCommandRequest request, CancellationToken cancellationToken)
    {
        return new(await payrollService.AddAsync(request,cancellationToken));
    }
}