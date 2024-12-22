using HrManagement.Domain.Entities.Identity;

namespace HrManagement.Domain.Entities;

public class Performance:BaseEntity
{
    public string Feedback { get; set; }
    public int WorkPerformanceScore { get; set; } 
    public int TeamworkScore { get; set; } 
    public int CommunicationScore { get; set; } 
    public int LeadershipScore { get; set; } 
    public int OverallScore { get; set; }
    public DateTime ReviewStartDate { get; set; }
    public DateTime ReviewEndDate { get; set; }
    
    public Guid ReviewedUserId { get; set; }
    public AppUser ReviewedUser { get; set; } 
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}