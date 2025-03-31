using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningPlatform.Application.Configurations;

namespace OnlineLearningPlatform.API.Extensions;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            // Tells the app to use JWT Bearer tokens for authentication
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            // Tells the app to challenge for JWT tokens when authentication fails (returns 401 Unauthorized, 403 Forbidden)
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // Sets JWT Bearer tokens as the default scheme for all authentication-related operations.
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // We’re using JWT Bearer tokens for authentication, and here’s how to validate them.
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.JwtKey!))
            };
        });
    }
}