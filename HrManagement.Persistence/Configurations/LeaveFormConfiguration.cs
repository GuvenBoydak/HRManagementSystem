using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrManagement.Persistence.Configurations;

public class LeaveFormConfiguration:IEntityTypeConfiguration<LeaveForm>
{
    public void Configure(EntityTypeBuilder<LeaveForm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.TotalDays).IsRequired();
        
        builder.HasOne(x=> x.Employee)
            .WithMany(e=> e.LeaveForms)
            .HasForeignKey(x=>x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.ApprovedUser)  
            .WithMany()  
            .HasForeignKey(e => e.ApprovedUserId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}