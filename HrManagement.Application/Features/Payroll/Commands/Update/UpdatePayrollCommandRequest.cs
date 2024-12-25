namespace HrManagement.Application.Features.Payroll.Commands.Update;

public record UpdatePayrollCommandRequest(Guid Id,
    decimal BasicSalary,
    decimal Allowances,
    decimal Overtime,
    decimal Deductions,
    decimal Tax,
    DateTime PaymentDate,
    string BankAccountNumber,
    string Comments,
    decimal RetirementFund):ICommand<UpdatePayrollCommandResponse>;