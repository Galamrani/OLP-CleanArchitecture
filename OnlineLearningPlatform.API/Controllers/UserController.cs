using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.Authentication;

namespace OnlineLearningPlatform.API.Controllers;

public class UserController(IAuthenticationService authenticationService) : ApiControllerBase
{
    private readonly IAuthenticationService authenticationService = authenticationService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        string token = await authenticationService.Register(registerDto);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CredentialsDto credentialsDto)
    {
        string token = await authenticationService.Login(credentialsDto);
        return Ok(token);
    }
}