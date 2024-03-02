using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MysticNet8App.Infrastructure.Persistance;

namespace MysticNet8App.Infrastructure;

public static class InfrastructureExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<RepositoryContext>(options =>
            options.UseMySql(ServerVersion.AutoDetect(connectionString)));
    }
    
}