using System.Net;
using AutoMapper;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Performance.Commands.Create;
using HrManagement.Application.Features.Performance.Commands.Delete;
using HrManagement.Application.Features.Performance.Commands.Update;
using HrManagement.Application.Features.Performance.Queries.GetAllPerformanceWithEmployeeId;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;
using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities;

namespace HrManagement.Application.Services;

public class PerformanceService(IPerformanceRepository performanceRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IPerformanceService
{
    public async Task<ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>>> GetAllPerformanceWithEmployeeIdAsync(
        Guid employeeId)
    {
        var performance = await performanceRepository.GetAsync(x => x.EmployeeId == employeeId,
            false,
            x => x.Employee);

        var performanceDto = mapper.Map<List<GetAllPerformanceWithEmployeeIdDto>>(performance);

        return ServiceResult<List<GetAllPerformanceWithEmployeeIdDto>>.Success(performanceDto);
    }

    public async Task<ServiceResult<GetPerformanceByIdDto>> GetPerformanceByIdAsync(Guid performanceId)
    {
        var performance = await performanceRepository.GetByIdAsync(performanceId,
            false,
            x => x.Employee);
        if (performance is null)
        {
            return ServiceResult<GetPerformanceByIdDto>.Failure(PerformanceConstant.NotFound, HttpStatusCode.NotFound);
        }

        var performanceDto = mapper.Map<GetPerformanceByIdDto>(performance);

        return ServiceResult<GetPerformanceByIdDto>.Success(performanceDto);
    }

    public async Task<ServiceResult<Guid>> AddAsync(CreatePerformanceCommandRequest request,
        CancellationToken cancellationToken)
    {
        var performance = mapper.Map<Performance>(request);

        await performanceRepository.AddAsync(performance, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<Guid>.SuccessAsCreated(performance.Id, $"api/performance/{performance.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(UpdatePerformanceCommandRequest request)
    {
        var performance = await performanceRepository.GetByIdAsync(request.Id);
        if (performance is null)
        {
            return ServiceResult.Failure(PerformanceConstant.NotFound, HttpStatusCode.NotFound);
        }

        var updatedPerformance = mapper.Map(request, performance);

        performanceRepository.Update(updatedPerformance);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(DeletePerformanceCommandRequest request)
    {
        var performance = await performanceRepository.GetByIdAsync(request.Id);
        if (performance is null)
        {
            return ServiceResult.Failure(PerformanceConstant.NotFound, HttpStatusCode.NotFound);
        }

        performanceRepository.Remove(performance);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}