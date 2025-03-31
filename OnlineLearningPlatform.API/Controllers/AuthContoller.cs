using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.Authentication;

namespace OnlineLearningPlatform.API.Controllers;

[AllowAnonymous]
[Route("api/v1/auth")]
public class AuthController(IAuthenticationService authenticationService) : ApiControllerBase
{
    private readonly IAuthenticationService authenticationService = authenticationService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        string token = await authenticationService.RegisterAsync(registerDto);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CredentialsDto credentialsDto)
    {
        string token = await authenticationService.LoginAsync(credentialsDto);
        return Ok(token);
    }
}