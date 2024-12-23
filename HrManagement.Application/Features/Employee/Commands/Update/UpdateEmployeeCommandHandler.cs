using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandHandler(IEmployeeService employeeService):ICommandHandler<UpdateEmployeeCommandRequest,UpdateEmployeeCommandResponse>
{
    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        return new UpdateEmployeeCommandResponse(await employeeService.UpdateAsync(request));
    }
}