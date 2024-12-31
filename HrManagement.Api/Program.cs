using FluentValidation;
using HrManagement.Api.ExceptionsHandler;
using HrManagement.Api.Extesions;
using HrManagement.Api.OptionsSetup;
using HrManagement.Application;
using HrManagement.Application.Extensions;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Infrastructure.Extensions;
using HrManagement.Infrastructure.Helpers;
using HrManagement.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerService();

builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.AddPersistenceServices(builder.Configuration).AddApplicationServices().AddInfrastructureServices();
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(x=>{});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using(var scoped = app.Services.CreateScope())
{
    var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    
    CreateAdminAndRole.CreateAdminAsync(userManager,roleManager).Wait();
}
app.Run();