using HrManagement.Application.Features.Employee.Commands.Create;
using HrManagement.Application.Features.Employee.Commands.Delete;
using HrManagement.Application.Features.Employee.Commands.Update;
using HrManagement.Application.Features.Employee.Queries.GetAllEmployee;
using HrManagement.Application.Features.Employee.Queries.GetEmployeeById;
using HrManagement.Domain.Shared;

namespace HrManagement.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<ServiceResult<List<GetAllEmployeeDto>>> GetAllAsync();
    Task<ServiceResult<PaginationResponse<List<GetAllEmployeeDto>>>> GetEmployeesBySearchWithPaginationAsync(string search,int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<ServiceResult<GetEmployeeByIdDto>> GetByIdAsync(Guid id);

    Task<ServiceResult<Guid>> AddAsync(CreateEmployeeCommandRequest commandRequest,
        CancellationToken cancellationToken);

    Task<ServiceResult> UpdateAsync(UpdateEmployeeCommandRequest request);
    Task<ServiceResult> DeleteAsync(DeleteEmployeeCommandRequest request);
}