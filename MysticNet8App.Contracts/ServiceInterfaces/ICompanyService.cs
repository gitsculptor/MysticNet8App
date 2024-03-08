using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Interfaces;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    CompanyDto GetCompanyById(Guid companyId, bool trackChanges);
    CompanyDto CreateCompany(CompanyInput company);
    void UpdateCompany(CompanyInput company,Guid id);
    void DeleteCompany(Guid id);
}