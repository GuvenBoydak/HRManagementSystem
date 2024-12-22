using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Enums;

namespace HrManagement.Domain.Entities;

public class LeaveForm:BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
    public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    public string Reason { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Guid? ApprovedUserId { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public AppUser ApprovedUser { get; set; }
    public Employee Employee { get; set; }
}
