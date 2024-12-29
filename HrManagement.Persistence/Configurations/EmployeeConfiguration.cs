using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrManagement.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Position).IsRequired().HasMaxLength(80);
        builder.Property(x => x.Department).IsRequired();
        builder.Property(x => x.Salary).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.PerformanceScore).HasColumnType("decimal(18, 2)");
        builder.Property(x => x.HireDate).IsRequired();
    }
}