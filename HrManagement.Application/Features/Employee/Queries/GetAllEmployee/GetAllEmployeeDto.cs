using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Queries.GetAllEmployee;

public record GetAllEmployeeDto(
    string Name,
    string Surname,
    string Email,
    string Phone,
    //EmployeeGender Gender,
    DateTime DateOfBirth,
    string Address,
    string Position,
    string Department,
    decimal Salary,
    DateTime HireDate,
    decimal PerformanceScore,
    int LeaveUsed,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    DateTime DeletedDate,
    bool IsDeleted);