using MysticAppNet8App.Domain.Models;

namespace MysticNet8App.Contracts.RepoInterfaces;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    Company? GetCompanyById(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    void UpdateCompany(Company company);
    void DeleteCompany(Guid id );
}