using HrManagement.Application.Behaviors;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HrManagement.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly));
        services.AddAutoMapper(typeof(ApplicationAssembly).Assembly);
        
        services.AddTransient(typeof(IPipelineBehavior<,>),(typeof(ValidationBehavior<,>)));
        
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ILeaveFormService, LeaveFormService>();
        services.AddScoped<IPerformanceService, PerformanceService>();
        services.AddScoped<IPayrollService, PayrollService>();
        
        return services;
    }
}