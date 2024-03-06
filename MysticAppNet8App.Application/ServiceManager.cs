using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Application.Services;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;

namespace MysticAppNet8App.Application;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    private readonly Lazy<IAuthenticationService> _authenticationService;


    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, UserManager<User> userManager)
    {
        _authenticationService =
            new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, logger, userManager));
        _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger, mapper));
        _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger, mapper));
    }

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;

    public IAuthenticationService AuthenticationService =>
        _authenticationService.Value;
}