using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrManagement.Persistence.Configurations;

public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BasicSalary).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.Allowances).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.Overtime).HasColumnType("decimal(18, 2)");
        builder.Property(x => x.Deductions).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.Tax).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.GrossSalary).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.NetSalary).IsRequired().HasColumnType("decimal(18, 2)");
        builder.Property(x => x.PaymentDate).IsRequired();
        builder.Property(x => x.PayPeriodStartDate).IsRequired();
        builder.Property(x => x.PayPeriodEndDate).IsRequired();
        builder.Property(x => x.BankAccountNumber).IsRequired();
        builder.Property(x => x.RetirementFund).IsRequired().HasColumnType("decimal(18, 2)");

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Payrolls)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}