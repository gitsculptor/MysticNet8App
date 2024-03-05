using Microsoft.AspNetCore.Mvc;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;

namespace MysticNet8App.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILoggerManager _logger;

    public EmployeeController(IServiceManager serviceManager, ILoggerManager logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpGet("/company/{id:Guid}/{eid:guid}")]
    public IActionResult GetEmployeeById(Guid id, Guid eid)
    {
        var result = _serviceManager.EmployeeService.GetEmployee(id, eid, true);
        return Ok(result);
    }

    [HttpGet("/company/{id:Guid}")]
    public IActionResult GetEmployeesByCompanyId(Guid id)
    {
        var result = _serviceManager.EmployeeService.GetEmployees(id, true);

        return Ok(result);
    }

    [HttpPost("/company/{id:guid}")]
    public IActionResult CreateEmployee(Guid id, [FromBody] EmployeeRequestDto employee)
    {
        var result = _serviceManager.EmployeeService.CreateEmployeeForCompany(id, employee, true);

        return Created();
    }
}