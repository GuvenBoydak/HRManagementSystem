namespace HrManagement.Application.Features.Performance.Commands.Create;

public record CreatePerformanceCommandRequest(
    string FeedBack,
    int WorkPerformanceScore,
    int TeamworkScore,
    int CommunicationScore,
    int LeadershipScore,
    DateTime ReviewStartDate,
    DateTime ReviewEndDate,
    Guid ReviewedUserId,
    Guid EmployeeId) : ICommand<CreatePerformanceCommandResponse>;