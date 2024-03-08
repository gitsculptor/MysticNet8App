using Mapster;
using MapsterMapper;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Exception;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager logger,
        IMapper mapper)
    {
        _logger = logger;
        _repository = repositoryManager;
        _mapper = mapper;
    }

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        var result = _repository.Employee.GetEmployees(companyId, true);

        return result.Adapt<IEnumerable<EmployeeDto>>();
    }

    public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        var result = _repository.Employee.GetEmployee(companyId, id, true);
        if (result is null)
            throw new ResourceNotFoundException(id, "employee");
        return result.Adapt<EmployeeDto>();
    }

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeRequestDto employeeForCreation,
        bool trackChanges)
    {
        var company = _repository.Company.GetCompanyById(companyId, trackChanges);
        if (company is null)
            throw new ResourceNotFoundException(companyId, "company");
        var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
        _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
        _repository.Save();
        var employeeToReturn = employeeEntity.Adapt<EmployeeDto>();
        return employeeToReturn;
    }
}