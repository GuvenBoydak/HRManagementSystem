using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;
using HrManagement.Application.Features.Payroll.Queries.GetPayrollById;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;
using HrManagement.Domain.Enums;

namespace HrManagement.Application.Features.Employee.Queries.GetEmployeeById;

public record GetEmployeeByIdDto(
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
    List<GetLeaveFormByIdDto> LeaveForms,
    List<GetPerformanceByIdDto> Performances,
    List<GetPayrollByIdDto> Payrolls);