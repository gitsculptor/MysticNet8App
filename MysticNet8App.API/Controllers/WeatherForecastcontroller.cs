using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysticNet8App.Contracts.Interfaces;

namespace MysticNet8App.Controllers;

[Route("api/[controller]")]
[ApiController]

public class WeatherForecastController : ControllerBase
{
    private static ILoggerManager _logger;
    public WeatherForecastController(ILoggerManager logger)
    {
        _logger = logger;
    }
    [HttpGet("{id}")]
    public IEnumerable<string> Get(long id)
    {
        _logger.LogInfo("Here is info message from our values controller.");
        _logger.LogDebug("Here is debug message from our values controller.");
        _logger.LogWarn("Here is warn message from our values controller.");
        _logger.LogError("Here is an error message from our values controller.");
        return new string[] { $"value1+{id}", "value2" };
    }
}