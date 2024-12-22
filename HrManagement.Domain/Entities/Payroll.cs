namespace HrManagement.Domain.Entities;

public class Payroll:BaseEntity
{
    public decimal BasicSalary { get; set; }
    public decimal Allowances { get; set; }
    public decimal Overtime { get; set; }
    public decimal Deductions { get; set; }
    public decimal Tax { get; set; }
    public decimal GrossSalary { get{ return BasicSalary + Allowances + Deductions + Tax;} set{ value = GrossSalary - Deductions - Tax;} }
    public decimal NetSalary{ get{ return GrossSalary - Deductions - Tax;}
        set{ value = GrossSalary - Deductions - Tax;}
    }
    public DateTime PaymentDate { get; set; }
    public DateTime PayPeriodStartDate { get; set; }
    public DateTime PayPeriodEndDate { get; set; }
    public string BankAccountNumber { get; set; }
    public string? Comments { get; set; }
    public decimal RetirementFund { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}