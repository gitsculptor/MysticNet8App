using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;

namespace MysticAppNet8App.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    public AuthenticationService(IMapper mapper, ILoggerManager logger, UserManager<User> userManager
    )
    {
        _mapper = mapper;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterUser(UserRegistrationDto
        userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user,
            userForRegistration.Password);
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
        return result;
    }
}