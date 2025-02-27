using System.Linq.Expressions;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
     Task<List<T>> GetAllAsync(bool isTracking = true,params Expression<Func<T, object>>[] includes);
     Task<T?> GetByIdAsync(Guid id, bool isTracking = true,params Expression<Func<T, object>>[] includes);
     Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = true,params Expression<Func<T, object>>[] includes);
     Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        bool isTracking = true);
    Task<int> CountAsync(CancellationToken cancellationToken);
    ValueTask AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Remove(T entity);
}