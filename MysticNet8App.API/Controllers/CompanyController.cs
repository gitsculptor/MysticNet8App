using Microsoft.AspNetCore.Mvc;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;

namespace MysticNet8App.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILoggerManager _logger;

    public CompanyController(IServiceManager serviceManager, ILoggerManager logger)
    {
        _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public IActionResult GetCompanyById(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompanyById(id, trackChanges: false);
        if (company == null)
            return NotFound();

        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] Company company)
    {
        if (company == null)
            return BadRequest("Company object is null");

        _serviceManager.CompanyService.CreateCompany(company);
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCompany(Guid id, [FromBody] Company company)
    {
        if (company == null)
            return BadRequest("Company object is null");

        if (id != company.Id)
            return BadRequest("Company Id mismatch");

        _serviceManager.CompanyService.UpdateCompany(company);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompanyById(id, trackChanges: false);
        if (company == null)
            return NotFound();

        _serviceManager.CompanyService.DeleteCompany(id);
        return NoContent();
    }
}