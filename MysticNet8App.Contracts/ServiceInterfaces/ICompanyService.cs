using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Interfaces;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    CompanyDto GetCompanyById(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    void UpdateCompany(Company company);
    void DeleteCompany(Guid id);
}