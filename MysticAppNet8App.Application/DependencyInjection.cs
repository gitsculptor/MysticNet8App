using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Application.Services;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Response;
using MysticNet8App.Infrastructure.Logging;

namespace MysticAppNet8App.Application;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IServiceManager,ServiceManager>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
       // services.AddScoped<ILoggerManager, LoggerManager>();
        services.AddScoped<UserManager<User>>();
      //  services.AddScoped<ILoggerManager, LoggerManager>();
       services.AddMapster();
       

    }
    
    private static void ConfigureMapsterMappings()
    {
        TypeAdapterConfig<Employee, EmployeeDto>.NewConfig()
            .MapWith(src => new EmployeeDto(src.Id, src.Name, src.Position, src.CompanyId,src.Age));
        // Add more mappings as needed
    }

   
    
}