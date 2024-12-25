using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Domain.Entities;
using HrManagement.Persistence.Contexts;

namespace HrManagement.Persistence.Repositories;

public class PayrollRepository(AppDbContext context) : GenericRepository<Payroll>(context), IPayrollRepository
{
}