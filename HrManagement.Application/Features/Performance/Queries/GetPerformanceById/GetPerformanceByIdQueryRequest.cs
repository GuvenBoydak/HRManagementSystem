namespace HrManagement.Application.Features.Performance.Queries.GetPerformanceById;

public record GetPerformanceByIdQueryRequest(Guid Id) : IQuery<GetPerformanceByIdQueryResponse>;