namespace HrManagement.Application.Features.Payroll.Commands.Create;

public record CreatePayrollCommandRequest(decimal BasicSalary,
    decimal Allowances,
    decimal Overtime,
    decimal Deductions,
    decimal Tax,
    DateTime PaymentDate,
    string BankAccountNumber,
    string Comments,
    decimal RetirementFund,
    Guid EmployeeId):ICommand<CreatePayrollCommandResponse>;