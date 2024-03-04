using Mapster;
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
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager,ServiceManager>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
      //  services.AddScoped<ILoggerManager, LoggerManager>();
       services.AddMapster();

    }

   
    
}