using Microsoft.AspNetCore.Mvc;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Exception;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

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

    [HttpGet("{id:guid}",Name = "CompanyById")]
    public IActionResult GetCompanyById(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompanyById(id, trackChanges: false);
        if (company is null)
            throw new ResourceNotFoundException(id, "company");

        return Ok(company);
    }

    [HttpPost]
    public IActionResult CreateCompany([FromBody] CompanyInput company)
    {
        var result = _serviceManager.CompanyService.CreateCompany(company);
        return CreatedAtRoute("CompanyById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCompany(Guid id, [FromBody] CompanyInput company)
    {
        _serviceManager.CompanyService.UpdateCompany(company, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCompany(Guid id)
    {
        _serviceManager.CompanyService.DeleteCompany(id);
        return NoContent();
    }
}