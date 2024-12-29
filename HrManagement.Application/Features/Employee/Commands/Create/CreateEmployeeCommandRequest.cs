using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Commands.Create;

public record CreateEmployeeCommandRequest(
    string Name,
    string Surname,
    string Email,
    string Phone,
    EmployeeGender Gender,
    DateTime DateOfBirth,
    string Address,
    string Position,
    EmployeeDepartment Department,
    decimal Salary,
    DateTime HireDate,
    Guid ManagerId) : ICommand<CreateEmployeeCommandResponse>;