using HrManagement.Application.Features.LeaveForm.Dtos;
using HrManagement.Application.Features.Payroll.Dtos;
using HrManagement.Application.Features.Performance.Dtos;
using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Queries.GetAllEmployee;

public record GetAllEmployeeDto(
    Guid Id,
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
    decimal PerformanceScore,
    int LeaveUsed,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    DateTime DeletedDate,
    bool IsDeleted,
    List<LeaveFormDto> LeaveForms,
    List<PerformanceDto> Performances,
    List<PayrollDto> Payrolls);