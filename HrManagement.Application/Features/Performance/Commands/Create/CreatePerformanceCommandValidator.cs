using FluentValidation;

namespace HrManagement.Application.Features.Performance.Commands.Create;

public class CreatePerformanceCommandValidator:AbstractValidator<CreatePerformanceCommandRequest>
{
    public CreatePerformanceCommandValidator()
    {
        RuleFor(p=> p.FeedBack).NotEmpty().WithMessage("Feedback is required");
        RuleFor(p=> p.WorkPerformanceScore).NotEmpty().WithMessage("Work performance score is required");
        RuleFor(p => p.WorkPerformanceScore)
            .InclusiveBetween(1, 10).WithMessage("WorkPerformanceScore score must be between 1 and 10");
        RuleFor(p=> p.TeamworkScore).NotEmpty().WithMessage("Teamwork score is required");
        RuleFor(p => p.WorkPerformanceScore)
            .InclusiveBetween(1, 10).WithMessage("TeamworkScore score must be between 1 and 10");
        RuleFor(p=> p.CommunicationScore).NotEmpty().WithMessage("Communication score is required");
        RuleFor(p => p.CommunicationScore)
            .InclusiveBetween(1, 10).WithMessage("CommunicationScore score must be between 1 and 10");
        RuleFor(p=> p.LeadershipScore).NotEmpty().WithMessage("Leadership score is required");
        RuleFor(p => p.LeadershipScore)
            .InclusiveBetween(1, 10).WithMessage("LeadershipScore score must be between 1 and 10");
        RuleFor(p=> p.ReviewStartDate).NotEmpty().WithMessage("Review start date is required");
        RuleFor(p=> p.ReviewEndDate).NotEmpty().WithMessage("Review end date is required");
        RuleFor(p=> p.ReviewedUserId).NotEmpty().WithMessage("Reviewed user id is required");
        RuleFor(p=> p.EmployeeId).NotEmpty().WithMessage("Employee id is required");
    }
}