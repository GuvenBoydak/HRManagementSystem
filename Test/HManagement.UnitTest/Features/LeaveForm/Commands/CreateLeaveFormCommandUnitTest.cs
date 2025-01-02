using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.LeaveForm.Commands.Create;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Commands;

public class CreateLeaveFormCommandUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly CreateLeaveFormCommandHandler _handler;

    public CreateLeaveFormCommandUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new CreateLeaveFormCommandHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenLeaveFormIsCreatedSuccessfully()
    {
        // Arrange
        var request = CreateLeaveFormCommandRequest();

        var leaveFormId = Guid.NewGuid();
        var serviceResult = ServiceResult<Guid>.SuccessAsCreated(leaveFormId, $"api/leaveform/{leaveFormId}");

        _leaveFormService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.Data.ShouldBe(leaveFormId);
        _leaveFormService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenLeaveFormCreationFails()
    {
        // Arrange
        var request = CreateLeaveFormCommandRequest();

        var serviceResult =
            ServiceResult<Guid>.Failure("Failed to save leave form", HttpStatusCode.InternalServerError);

        _leaveFormService
            .Setup(s => s.AddAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _leaveFormService.Verify(s => s.AddAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    private CreateLeaveFormCommandRequest CreateLeaveFormCommandRequest()
    {
        return new CreateLeaveFormCommandRequest(
            StartDate: new DateTime(2023, 1, 1),
            EndDate: new DateTime(2023, 1, 10),
            TotalDays: 10,
            Status: LeaveStatus.Pending,
            Reason: "Vacation",
            EmployeeId: Guid.NewGuid());
    }
}