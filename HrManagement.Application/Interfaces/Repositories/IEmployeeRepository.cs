using HrManagement.Domain.Entities;
using HrManagement.Domain.Shared;

namespace HrManagement.Application.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<List<Employee>> GetEmployeesBySearchWithPaginationAsync(string search,int pageNumber, int pageSize);
}