using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.LeaveForm.Queries.GetLeaveFormById;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.LeaveForm.Queries;

public class GetLeaveFormByIdQueryUnitTest
{
    private readonly Mock<ILeaveFormService> _leaveFormService;
    private readonly GetLeaveFormByIdQueryHandler _handler;

    public GetLeaveFormByIdQueryUnitTest()
    {
        _leaveFormService = new Mock<ILeaveFormService>();
        _handler = new GetLeaveFormByIdQueryHandler(_leaveFormService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenLeaveFormsRetrievedSuccessfully()
    {
        // Arrange 
        var request = new GetLeaveFormByIdQueryRequest(Id: Guid.NewGuid());
        var leaveForm = CreateLeaveFormDto();

        var serviceResult = ServiceResult<GetLeaveFormByIdDto>.Success(leaveForm);
        _leaveFormService
            .Setup(s => s.GetByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _leaveFormService.Verify(s => s.GetByIdAsync(request.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoLeaveFormFound()
    {
        // Arrange 
        var request = new GetLeaveFormByIdQueryRequest(Id: Guid.NewGuid());

        var serviceResult = ServiceResult<GetLeaveFormByIdDto>.Failure(LeaveFormConstant.NotFound);
        _leaveFormService
            .Setup(s => s.GetByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _leaveFormService.Verify(s => s.GetByIdAsync(request.Id), Times.Once);
    }

    private GetLeaveFormByIdDto CreateLeaveFormDto()
    {
        return new GetLeaveFormByIdDto(
            Id: Guid.NewGuid(),
            StartDate: new DateTime(2023, 1, 1),
            EndDate: new DateTime(2023, 1, 10),
            TotalDays: 10,
            Status: LeaveStatus.Pending,
            Reason: "Vacation",
            EmployeeId: Guid.NewGuid(),
            ApprovedUserId: Guid.NewGuid(),
            ApprovalDate: DateTime.Now,
            CreatedDate: DateTime.Now,
            UpdatedDate: DateTime.Now,
            DeletedDate: DateTime.MinValue,
            IsDeleted: false,
            Employee: new EmployeeDto(
                Id: Guid.NewGuid(),
                Name: "John",
                Surname: "Doe",
                Email: "john.doe@example.com",
                Phone: "123-456-7890",
                Gender: EmployeeGender.Male,
                DateOfBirth: new DateTime(1990, 1, 1),
                Address: "123 Main St",
                Position: "Developer",
                Department: EmployeeDepartment.Employee,
                Salary: 60000m,
                HireDate: new DateTime(2020, 1, 1),
                PerformanceScore: 85.5m,
                LeaveUsed: 5,
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false)
        );
    }
}