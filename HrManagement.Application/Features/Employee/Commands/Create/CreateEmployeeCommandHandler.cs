using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Commands.Create;

public class CreateEmployeeCommandHandler(IEmployeeService employeeService):ICommandHandler<CreateEmployeeCommandRequest,CreateEmployeeCommandResponse>
{
    public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
       return new CreateEmployeeCommandResponse(await employeeService.AddAsync(request, cancellationToken));
    }
}