using HrManagement.Api.Extesions;
using HrManagement.Api.OptionsSetup;
using HrManagement.Application.Extensions;
using HrManagement.Infrastructure.Extensions;
using HrManagement.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerService();

builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.AddPersistenceServices(builder.Configuration).AddApplicationServices().AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();