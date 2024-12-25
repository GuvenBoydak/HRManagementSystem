using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrManagement.Persistence.Configurations;

public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
{
    public void Configure(EntityTypeBuilder<Performance> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.WorkPerformanceScore).IsRequired();
        builder.Property(x => x.TeamworkScore).IsRequired();
        builder.Property(x => x.LeadershipScore).IsRequired();
        builder.Property(x => x.CommunicationScore).IsRequired();
        builder.Property(x => x.ReviewStartDate).IsRequired();
        builder.Property(x => x.ReviewEndDate).IsRequired();
        builder.Ignore(p => p.OverallScore);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Performances)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReviewedUser)
            .WithMany()
            .HasForeignKey(e => e.ReviewedUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}