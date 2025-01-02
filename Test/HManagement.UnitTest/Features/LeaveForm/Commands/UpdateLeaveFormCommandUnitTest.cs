using System.Net;
using HrManagement.Application;
using HrManagement.Application.Features.LeaveForm.Commands.Update;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Commands;

public class UpdateLeaveFormCommandUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly UpdateLeaveFormCommandHandler _handler;

    public UpdateLeaveFormCommandUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new UpdateLeaveFormCommandHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenLeaveFormIsUpdatededSuccessfully()
    {
        // Arrange
        var request = UpdateLeaveFormCommandRequest();

        var serviceResult = ServiceResult.Success(HttpStatusCode.NoContent);
        _leaveFormService.Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _leaveFormService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResponse_WhenLeaveFormUpdateFails()
    {
        // Arrange
        var request = UpdateLeaveFormCommandRequest();

        var serviceResult = ServiceResult.Failure("Failed to update leave form", HttpStatusCode.InternalServerError);
        _leaveFormService.Setup(s => s.UpdateAsync(request))
            .ReturnsAsync(serviceResult);

        //Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _leaveFormService.Verify(s => s.UpdateAsync(request), Times.Once);
    }

    private UpdateLeaveFormCommandRequest UpdateLeaveFormCommandRequest()
    {
        return new UpdateLeaveFormCommandRequest(
            Id: Guid.NewGuid(),
            StartDate: new DateTime(2023, 1, 1),
            EndDate: new DateTime(2023, 1, 10),
            TotalDays: 10,
            Status: LeaveStatus.Pending,
            Reason: "Vacation");
    }
}