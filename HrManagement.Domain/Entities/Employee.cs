using HrManagement.Domain.Enums;

namespace HrManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public EmployeeGender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public EmployeeDepartment Department { get; set; } = 0;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public decimal PerformanceScore { get; set; }
    public int LeaveUsed { get; set; }

    public ICollection<LeaveForm> LeaveForms { get; set; }
    public ICollection<Performance> Performances { get; set; }
    public ICollection<Payroll> Payrolls { get; set; }
}