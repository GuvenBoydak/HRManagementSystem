using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Performance.Commands.Update;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Performance.Commands;

public class UpdatePerformanceCommandUnitTest
{
    private readonly Mock<IPerformanceService> _performanceService;
    private readonly UpdatePerformanceCommandHandler _handler;

    public UpdatePerformanceCommandUnitTest()
    {
        _performanceService = new Mock<IPerformanceService>();
        _handler = new UpdatePerformanceCommandHandler(_performanceService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPerformanceIsUpdatedSuccessfully()
    {
        // Arrange
        var request = CreatePerformanceRequest();

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _performanceService
            .Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _performanceService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenPerformanceUpdateFails()
    {
        // Arrange
        var request = CreatePerformanceRequest();

        var serviceResult = ServiceResult.Failure("Failed to update performance", HttpStatusCode.InternalServerError);
        _performanceService
            .Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeEmpty();
        _performanceService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    private UpdatePerformanceCommandRequest CreatePerformanceRequest()
    {
        return new UpdatePerformanceCommandRequest(
            Id: Guid.NewGuid(),
            FeedBack: "Excellent work throughout the year.",
            WorkPerformanceScore: 8,
            TeamworkScore: 9,
            CommunicationScore: 8,
            LeadershipScore: 9,
            ReviewStartDate: new DateTime(2023, 1, 1),
            ReviewEndDate: new DateTime(2023, 12, 31),
            ReviewedUserId: Guid.NewGuid());
    }
}