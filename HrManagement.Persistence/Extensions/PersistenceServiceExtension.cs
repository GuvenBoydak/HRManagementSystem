using HrManagement.Domain.Shared;
using HrManagement.Persistence.Contexts;
using HrManagement.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrManagement.Persistence.Extensions;

public static class PersistenceServiceExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseSqlServer(connectionString!.SqlServer, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });
            options.AddInterceptors(new EntityDbContextInterceptor());
        });
        return services;
    }
}