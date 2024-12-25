namespace HrManagement.Application.Features.Payroll.Queries.GetPayrollById;

public record GetPayrollByIdDto(Guid Id,
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