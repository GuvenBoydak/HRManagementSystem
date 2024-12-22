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
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/HrManagement.Api"));
        configurationManager.AddJsonFile("appsettings.json");
        
        var connectionString = configurationManager.GetConnectionString(ConnectionStringOption.Key);
        optionsBuilder.UseSqlServer(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}