using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.RepoInterfaces;

namespace MysticNet8App.Infrastructure.Persistance;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
    {
        var result = FindByCondition(x => x.CompanyId == companyId, true).ToList();

        return result;
    }

    public Employee? GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        var result = FindByCondition(x => x.CompanyId == companyId && x.Id==id, true).SingleOrDefault();
        return result;
    }

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
}