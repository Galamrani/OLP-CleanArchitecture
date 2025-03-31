using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.UserManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
[Route("users/me")]
public class UsersController(IUserService userService) : ApiControllerBase
{
    private readonly IUserService userService = userService;

    [HttpGet("courses")]
    // NOTE: fetch basic created courses - no lessons
    public async Task<IActionResult> GetCreatedCourses()
    {
        List<CourseDto> courses = await userService.GetUserCreatedCoursesAsync(GetUserId(HttpContext));
        return Ok(courses);
    }

    [HttpGet("enrollments")]
    // NOTE: fetch basic enrolled courses - no lessons
    public async Task<IActionResult> GetEnrolledCourses()
    {
        List<CourseDto> courses = await userService.GetUserEnrolledCoursesAsync(GetUserId(HttpContext));
        return Ok(courses);
    }
}