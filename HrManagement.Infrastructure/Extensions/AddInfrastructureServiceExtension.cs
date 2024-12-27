using System.Text;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Infrastructure.Service.Auth;
using HrManagement.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HrManagement.Infrastructure.Extensions;

public static class AddInfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();
        
        services.AddScoped<IJwtProviderService, JwtProviderService>();
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}