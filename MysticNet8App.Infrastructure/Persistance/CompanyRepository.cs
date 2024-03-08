using Microsoft.EntityFrameworkCore;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.RepoInterfaces;

namespace MysticNet8App.Infrastructure.Persistance;

public class CompanyRepository(RepositoryContext repositoryContext) : RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    
    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
   
    public Company? GetCompanyById(Guid companyId, bool trackChanges) =>
        FindByCondition(c => c.Id == companyId, trackChanges)
            .SingleOrDefault();

    public void CreateCompany(Company company) =>
        Create(company);

    public void UpdateCompany(Company company) =>
        Update(company);

    public void DeleteCompany(Guid id)
    {
        var companyToDelete = FindByCondition(c => c.Id == id, trackChanges: false).SingleOrDefault();
        if (companyToDelete != null)
        {
            Delete(companyToDelete);
        }
    }
}