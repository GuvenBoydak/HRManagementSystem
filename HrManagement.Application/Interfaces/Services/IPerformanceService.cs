using HrManagement.Application.Features.Performance.Commands.Create;
using HrManagement.Application.Features.Performance.Commands.Delete;
using HrManagement.Application.Features.Performance.Commands.Update;
using HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;

namespace HrManagement.Application.Interfaces.Services;

public interface IPerformanceService
{
    Task<ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>>> GetAllPerformanceWithEmployeeIdAsync(Guid employeeId);
    Task<ServiceResult<GetPerformanceByIdDto>> GetPerformanceByIdAsync(Guid performanceId);
    Task<ServiceResult<Guid>> AddAsync(CreatePerformanceCommandRequest request, CancellationToken cancellationToken);
    Task<ServiceResult> UpdateAsync(UpdatePerformanceCommandRequest request);
    Task<ServiceResult> DeleteAsync(DeletePerformanceCommandRequest request);
}