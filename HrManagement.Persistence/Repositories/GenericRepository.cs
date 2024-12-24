using System.Linq.Expressions;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Domain.Entities;
using HrManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Persistence.Repositories;

public class GenericRepository<T>:IRepository<T> where T:BaseEntity
{
    protected readonly DbSet<T> _entity;
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }
    public async Task<List<T>> GetAllAsync(bool isTracking = true)
    {
        if(isTracking)
        {
            return await _entity.AsNoTracking().ToListAsync();
        }
        return await _entity.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, bool isTracking = true)
    {
        if (isTracking)
        {
            return await _entity.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);
        }
        return  await _entity.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
    {
        if (isTracking)
        {
            return await _entity.AsNoTracking().Where(predicate).ToListAsync();
        }
        return await _entity.Where(predicate).ToListAsync();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        bool isTracking = true)
    {
        if (isTracking)
        {
            return await _entity.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        return await _entity.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async ValueTask AddAsync(T entity, CancellationToken cancellationToken)
    { 
        await _entity.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _entity.Update(entity);
    }

    public void Remove(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        Update(entity);
    }
}