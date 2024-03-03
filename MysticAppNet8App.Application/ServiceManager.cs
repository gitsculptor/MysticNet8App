using MapsterMapper;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Application.Services;
using MysticNet8App.Contracts.Interfaces;

namespace MysticAppNet8App.Application;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly IMapper _mapper;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper)
    {
        _mapper = mapper;
        _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger,mapper));
        _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger));
    }

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
}