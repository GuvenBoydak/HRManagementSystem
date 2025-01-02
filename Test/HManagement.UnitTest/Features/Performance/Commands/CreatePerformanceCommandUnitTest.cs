using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.Performance.Commands.Create;
using HrManagement.Application.Interfaces.Services;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Performance.Commands;

public class CreatePerformanceCommandUnitTest
{
    private readonly Mock<IPerformanceService> _performanceService;
    private readonly CreatePerformanceCommandHandler _handler;

    public CreatePerformanceCommandUnitTest()
    {
        _performanceService = new Mock<IPerformanceService>();
        _handler = new CreatePerformanceCommandHandler(_performanceService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPerformanceIsCreatedSuccessfully()
    {
        // Arrange
        var request = CreatePerformanceRequest();

        var performanceId = Guid.NewGuid();
        var serviceResult = ServiceResult<Guid>.SuccessAsCreated(performanceId, $"api/performance/{performanceId}");
        _performanceService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _performanceService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenPerformanceCreationFails()
    {
        // Arrange
        var request = CreatePerformanceRequest();
        
        var serviceResult =
            ServiceResult<Guid>.Failure("Failed to save performance", HttpStatusCode.InternalServerError);
        _performanceService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _performanceService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    private CreatePerformanceCommandRequest CreatePerformanceRequest()
    {
        return new CreatePerformanceCommandRequest(
            FeedBack: "Excellent work throughout the year.",
            WorkPerformanceScore: 8,
            TeamworkScore: 9,
            CommunicationScore: 8,
            LeadershipScore: 9,
            ReviewStartDate: new DateTime(2023, 1, 1),
            ReviewEndDate: new DateTime(2023, 12, 31),
            ReviewedUserId: Guid.NewGuid(),
            EmployeeId: Guid.NewGuid());
    }
}