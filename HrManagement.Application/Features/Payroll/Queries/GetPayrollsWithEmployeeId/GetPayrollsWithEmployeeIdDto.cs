namespace HrManagement.Application.Features.Payroll.Queries.GetPayrollsWithEmployeeId;

public record GetPayrollsWithEmployeeIdDto(Guid Id,
    decimal BasicSalary,
    decimal Allowances,
    decimal Overtime,
    decimal Deductions,
    decimal Tax,
    decimal GrossSalary,
    decimal NetSalary,
    DateTime PaymentDate,
    string BankAccountNumber,
    string Comments,
    decimal RetirementFund,
    Guid EmployeeId);