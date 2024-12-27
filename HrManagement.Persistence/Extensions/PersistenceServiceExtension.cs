using HrManagement.Application.Interfaces;
using HrManagement.Application.Interfaces.Repositories;
using HrManagement.Domain.Shared;
using HrManagement.Persistence.Contexts;
using HrManagement.Persistence.Interceptors;
using HrManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrManagement.Persistence.Extensions;

public static class PersistenceServiceExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseSqlServer(connectionString!.SqlServer,
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                });
            options.AddInterceptors(new EntityDbContextInterceptor());
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPayrollRepository, PayrollRepository>();
        services.AddScoped<IPerformanceRepository, PerformanceRepository>();
        services.AddScoped<ILeaveFormRepository, LeaveFormRepository>();

        return services;
    }
}