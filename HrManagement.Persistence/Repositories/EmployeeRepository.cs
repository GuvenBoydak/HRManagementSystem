using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Domain.Entities;
using HrManagement.Domain.Shared;
using HrManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Persistence.Repositories;

public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository
{
    public async Task<List<Employee>> GetEmployeesBySearchWithPaginationAsync(string search, int pageNumber, int pageSize)
    {
        var query = _entity.AsQueryable();
    
        // Eğer search boş değilse arama yapılacak
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, "%" + search + "%"));
        }
    
        // Sayfalama işlemi
        return await query
            .Skip((pageNumber - 1) * pageSize)  // Sayfalama: Bir önceki sayfa kadar atlama yapar
            .Take(pageSize)  // Belirtilen sayıda veri çeker
            .ToListAsync();  // Sonuçları listeye çevirir
    }

}