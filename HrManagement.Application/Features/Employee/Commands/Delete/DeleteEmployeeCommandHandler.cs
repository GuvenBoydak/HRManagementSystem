using HrManagement.Application.Interfaces.Services;

namespace HrManagement.Application.Features.Employee.Commands.Delete;

public class DeleteEmployeeCommandHandler(IEmployeeService employeeService)
    : ICommandHandler<DeleteEmployeeCommandRequest, DeleteEmployeeCommandResponse>
{
    public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new DeleteEmployeeCommandResponse(await employeeService.DeleteAsync(request));
    }
}