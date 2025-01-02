using System.Net;
using HrManagement.Application;
using HrManagement.Application.Constant;
using HrManagement.Application.Features.Employee.Dtos;
using HrManagement.Application.Features.Performance.Queries.GetPerformanceById;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Enums;
using Moq;
using Shouldly;

namespace HManagement.UnitTest.Features.Performance.Queries;

public class GetPerformanceByIdQueryUnitTest
{
    private readonly Mock<IPerformanceService> _performanceService;
    private readonly GetPerformanceByIdQueryHandler _handler;

    public GetPerformanceByIdQueryUnitTest()
    {
        _performanceService = new Mock<IPerformanceService>();
        _handler = new GetPerformanceByIdQueryHandler(_performanceService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResponse_WhenPerformanceRetrievedSuccessfully()
    {
        // Arrange 
        var request = new GetPerformanceByIdQueryRequest(Id: Guid.NewGuid());
        var performanceDto = GeneratePerformanceDtos();

        var serviceResult = ServiceResult<GetPerformanceByIdDto>.Success(performanceDto);
        _performanceService
            .Setup(s => s.GetPerformanceByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.IsSuccess.ShouldBeTrue();
        _performanceService.Verify(s => s.GetPerformanceByIdAsync(request.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmpty_WhenNoPerformanceFound()
    {
        // Arrange 
        var request = new GetPerformanceByIdQueryRequest(Id: Guid.NewGuid());

        var serviceResult =
            ServiceResult<GetPerformanceByIdDto>.Failure(PerformanceConstant.NotFound,
                HttpStatusCode.InternalServerError);
        _performanceService
            .Setup(s => s.GetPerformanceByIdAsync(request.Id))
            .ReturnsAsync(serviceResult);

        // Act 
        var response = await _handler.Handle(request, default);

        // Assert
        response.ShouldNotBeNull();
        response.Result.ShouldBe(serviceResult);
        response.Result.ErrorMessages.ShouldNotBeNull();
        _performanceService.Verify(s => s.GetPerformanceByIdAsync(request.Id), Times.Once);
    }

    private GetPerformanceByIdDto GeneratePerformanceDtos()
    {
        return new GetPerformanceByIdDto(
            Id: Guid.NewGuid(),
            FeedBack: "Excellent work",
            WorkPerformanceScore: 9,
            TeamworkScore: 8,
            CommunicationScore: 8,
            LeadershipScore: 8,
            OverallScore: 8,
            ReviewStartDate: new DateTime(2023, 1, 1),
            ReviewEndDate: new DateTime(2023, 6, 30),
            ReviewedUserId: Guid.NewGuid(),
            EmployeeId: Guid.NewGuid(),
            CreatedDate: DateTime.UtcNow,
            UpdatedDate: DateTime.UtcNow,
            DeletedDate: DateTime.MinValue,
            IsDeleted: false,
            Employee: new EmployeeDto(
                Id: Guid.NewGuid(),
                Name: "Jane",
                Surname: "Doe",
                Email: "jane.doe@example.com",
                Phone: "123-456-7890",
                Gender: EmployeeGender.Female,
                DateOfBirth: new DateTime(1992, 5, 15),
                Address: "456 Elm St",
                Position: "Manager",
                Department: EmployeeDepartment.Employee,
                Salary: 80000m,
                HireDate: new DateTime(2018, 3, 1),
                PerformanceScore: 9,
                LeaveUsed: 2,
                CreatedDate: DateTime.UtcNow,
                UpdatedDate: DateTime.UtcNow,
                DeletedDate: DateTime.MinValue,
                IsDeleted: false)
        );
    }
}