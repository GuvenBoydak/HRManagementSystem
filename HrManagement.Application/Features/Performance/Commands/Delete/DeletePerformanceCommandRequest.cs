namespace HrManagement.Application.Features.Performance.Commands.Delete;

public record DeletePerformanceCommandRequest(Guid Id):ICommand<DeletePerformanceCommandResponse>;