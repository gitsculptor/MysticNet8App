namespace MysticAppNet8App.Domain;

public class JwtConfiguration
{
    public string Section { get; init; } = "JwtSettings";
    public  string ValidIssuer { get; init; }
    public  string ValidAudience { get; init; }
    public  string Expires { get; init; }
    public  string SecretKey { get; init; }
}