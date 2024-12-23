using HrManagement.Domain.Shared;
using HrManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace HrManagement.Persistence;

public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        
        ConfigurationManager configurationManager = new();
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HrManagement.Api"));
        configurationManager.AddJsonFile("appsettings.json");
        
        var connectionString = configurationManager.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
        optionsBuilder.UseSqlServer(connectionString!.SqlServer);
        return new AppDbContext(optionsBuilder.Options);
    }
}