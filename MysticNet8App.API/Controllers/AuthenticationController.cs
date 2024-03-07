using Microsoft.AspNetCore.Mvc;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;

namespace MysticNet8App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly ILoggerManager _logger;

    public AuthenticationController(IServiceManager serviceManager, ILoggerManager logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto
        userForRegistration)
    {
        var result = await
            _serviceManager.AuthenticationService.RegisterUser(userForRegistration);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
        user)
    {
        if (!await _serviceManager.AuthenticationService.ValidateUser(user))
            return Unauthorized();
        return Ok(new { Token = await _serviceManager
            .AuthenticationService.CreateToken() });
    }
}