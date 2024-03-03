using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.RepoInterfaces;

namespace MysticNet8App.Infrastructure.Persistance;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public RepositoryManager(RepositoryContext repositoryContext, ICompanyRepository companyRepository, IEmployeeRepository employeeRepository)
    {
        _repositoryContext = repositoryContext;
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
    }

    public ICompanyRepository Company => _companyRepository;
    public IEmployeeRepository Employee => _employeeRepository;
    public void Save() => _repositoryContext.SaveChanges();
}