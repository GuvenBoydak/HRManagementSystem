namespace HrManagement.Application.Features.Employee.Commands.Delete;

public record DeleteEmployeeCommandRequest(Guid Id) : ICommand<DeleteEmployeeCommandResponse>;