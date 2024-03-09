using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MysticAppNet8App.Application.Interfaces;
using MysticAppNet8App.Domain;
using MysticAppNet8App.Domain.Models;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Contracts.Request;
using MysticNet8App.Contracts.Response;

namespace MysticAppNet8App.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private User? _user;
    private readonly JwtConfiguration _configuration;


    public AuthenticationService(IMapper mapper, ILoggerManager logger, UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
    {
        _mapper = mapper;
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration.Value;
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

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user,
            userForAuth.Password));
        if (!result)
            _logger.LogError($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
        return result;
    }

   
    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;
        if(populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto(accessToken, refreshToken);
    }
    
    private SigningCredentials GetSigningCredentials()
    {
        string secretKey = "letsseeauthenticationinactionforrealseeiftiworkds";
        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _configuration.ValidIssuer,
            audience: _configuration.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.Expires)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
    
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    
    
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        // var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
            ValidateLifetime = true,
            ValidIssuer = _configuration.ValidIssuer,
            ValidAudience = _configuration.ValidAudience,
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out
            securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }
}