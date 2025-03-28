using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.Authentication;
using OnlineLearningPlatform.Application.Services.UserManagement;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.API.Controllers;

[AllowAnonymous]
public class UserController(IUserService userService, IAuthenticationService authenticationService) : ApiControllerBase
{
    private readonly IUserService userService = userService;

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

    [Authorize]
    [HttpPost("enroll/{courseId}")]
    public async Task<IActionResult> EnrollToCourse(Guid courseId)
    {
        CourseDto course = await userService.EnrollToCourseAsync(GetUserId(HttpContext), courseId);
        return Created(string.Empty, course);
    }

    [Authorize]
    [HttpDelete("unenroll/{courseId}")]
    public async Task<IActionResult> UnenrollToCourse(Guid courseId)
    {
        await userService.UnenrollToCourseAsync(GetUserId(HttpContext), courseId);
        return NoContent();
    }
}