using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HrManagement.Persistence.Interceptors;

public class EntityDbContextInterceptor:SaveChangesInterceptor
{
    private readonly Dictionary<EntityState, Action<DbContext,BaseEntity>> _auditActions = new()
    {
        { EntityState.Added,AddBehaivor},
        { EntityState.Modified,UpdateBehaivor}
    };
    private static void AddBehaivor(DbContext context, BaseEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        context.Entry(entity).Property(x=> x.UpdatedDate).IsModified = false;
    }
    private static void UpdateBehaivor(DbContext context, BaseEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
    }
    private static void DeleteBehaivor(DbContext context, BaseEntity entity)
    {
        entity.DeletedDate = DateTime.UtcNow;
        entity.IsDeleted = true;
        context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        context.Entry(entity).Property(x=> x.UpdatedDate).IsModified = false;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries())
        {
            if(entityEntry.Entity is not BaseEntity entity) continue;
            
            _auditActions[entityEntry.State](eventData.Context, entity);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}