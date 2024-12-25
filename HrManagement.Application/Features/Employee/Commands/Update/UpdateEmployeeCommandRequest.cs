using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Commands.Update;

public record UpdateEmployeeCommandRequest(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string Phone,
    EmployeeGender Gender,
    DateTime DateOfBirth,
    string Address,
    string Position,
    string Department,
    decimal Salary,
    DateTime HireDate,
    decimal PerformanceScore,
    int LeaveUsed,
    Guid ManagerId) : ICommand<UpdateEmployeeCommandResponse>;