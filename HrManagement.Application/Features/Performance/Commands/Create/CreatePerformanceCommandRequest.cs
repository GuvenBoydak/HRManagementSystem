namespace HrManagement.Application.Features.Performance.Commands.Create;

public record CreatePerformanceCommandRequest(
    string FeedBack,
    int WorkPerformanceScore,
    int TeamworkScore,
    int CommunicationScore,
    int LeadershipScore,
    DateTime ReviewStartDate,
    Guid EmployeeId) : ICommand<CreatePerformanceCommandResponse>;