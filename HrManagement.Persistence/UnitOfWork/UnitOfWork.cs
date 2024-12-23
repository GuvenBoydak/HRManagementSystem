using HrManagement.Application.Interfaces;
using HrManagement.Persistence.Contexts;

namespace HrManagement.Persistence.UnitOfWork;

public class UnitOfWork(AppDbContext context):IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}