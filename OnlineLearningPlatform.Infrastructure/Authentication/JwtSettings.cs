namespace OnlineLearningPlatform.Infrastructure.Authentication;

public class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string JwtKey { get; set; } = string.Empty;
    public int Lifetime { get; set; }
}