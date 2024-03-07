using System.ComponentModel.DataAnnotations;

namespace MysticNet8App.Contracts.Request;


public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Password name is required")]
    public string? Password { get; init; }
}