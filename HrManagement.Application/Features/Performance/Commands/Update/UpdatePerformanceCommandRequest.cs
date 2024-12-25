namespace HrManagement.Application.Features.Performance.Commands.Update;

public record UpdatePerformanceCommandRequest(Guid Id,
    string FeedBack,
    int WorkPerformanceScore,
    int TeamworkScore,
    int CommunicationScore,
    int LeadershipScore,
    DateTime ReviewStartDate,
    DateTime ReviewEndDate,
    Guid ReviewedUserId):ICommand<UpdatePerformanceCommandResponse>;