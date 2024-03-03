using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.RepoInterfaces;
using MysticNet8App.Infrastructure.Logging;
using MysticNet8App.Infrastructure.Persistance;

namespace MysticNet8App.Infrastructure;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("sqlConnection");

        Console.WriteLine($"string:{connectionString}");
        services.AddDbContext<RepositoryContext>(options =>
                options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

       // services.AddScoped<ILoggerManager, LoggerManager>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<ICompanyRepository,CompanyRepository>();
        services.AddScoped<IEmployeeRepository,EmployeeRepository>();
    }
    
}