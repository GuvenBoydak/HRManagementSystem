using System.ComponentModel.DataAnnotations.Schema;

namespace HrManagement.Domain.Entities;

public class Payroll : BaseEntity
{
    public decimal BasicSalary { get; set; }
    public decimal Allowances { get; set; }
    public decimal Overtime { get; set; }
    public decimal Deductions { get; set; }
    public decimal Tax { get; set; }
    public DateTime PaymentDate { get; set; }
    public string BankAccountNumber { get; set; }
    public string? Comments { get; set; }
    public decimal RetirementFund { get; set; }
    //[NotMapped]
    public decimal GrossSalary => BasicSalary + Allowances + Deductions + Tax + Overtime;
    //[NotMapped]
    public decimal NetSalary => GrossSalary - Deductions - Tax;
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}