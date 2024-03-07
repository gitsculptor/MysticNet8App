using Microsoft.AspNetCore.Identity;
using MysticNet8App.Contracts.Request;

namespace MysticAppNet8App.Application.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserRegistrationDto userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
    
}