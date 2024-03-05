using Mapster;
using MapsterMapper;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Exception;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies =
            _repository.Company.GetAllCompanies(trackChanges);

        var result = _mapper.Map<List<CompanyDto>>(companies);

        return result;
    }

    public CompanyDto GetCompanyById(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompanyById(companyId, trackChanges);
        if (company == null)
        {
            _logger.LogError($"Company with id: {companyId} was not found in the database.");
            throw new InvalidOperationException($"Company with id: {companyId} was not found in the database.");
        }

        var result = _mapper.Map<CompanyDto>(company);
        return result;
    }

    public CompanyDto CreateCompany(CompanyInput companyInput)
    {
        var company = _mapper.Map<Company>(companyInput);
        company.Id = Guid.NewGuid();
        _repository.Company.CreateCompany(company);
        _repository.Save();
        return _mapper.Map<CompanyDto>(company);
    }

    public void UpdateCompany(CompanyInput company, Guid id)
    {
        var x = _repository.Company.GetCompanyById(id, true);
        if (x is null)
            throw new ResourceNotFoundException(id, "company");

        x.Address = company.Address;
        x.Country = x.Name;
        _repository.Company.UpdateCompany(x);
        _repository.Save();
    }

    public void DeleteCompany(Guid id)
    {
        _repository.Company.DeleteCompany(id);
        _repository.Save();
    }
}