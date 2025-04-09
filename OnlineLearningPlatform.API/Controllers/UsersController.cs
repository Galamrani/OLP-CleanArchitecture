using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.UserManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
[Route("api/v1/users/{userId}")]
public class UsersController(IUserService userService) : ApiControllerBase
{
    private readonly IUserService userService = userService;

    [HttpGet("courses")]
    public async Task<IActionResult> GetCreatedCourses(Guid userId)
    {
        if (userId != GetUserId(HttpContext)) return Forbid();
        List<CourseDto> courses = await userService.GetUserCreatedCoursesAsync(userId);
        return Ok(courses);
    }

    [HttpGet("enrollments")]
    public async Task<IActionResult> GetEnrolledCourses(Guid userId)
    {
        if (userId != GetUserId(HttpContext)) return Forbid();
        List<CourseDto> courses = await userService.GetUserEnrolledCoursesAsync(userId);
        return Ok(courses);
    }

    [HttpGet("enrollments/{courseId}")]
    public async Task<IActionResult> GetEnrolledCourse(Guid userId, Guid courseId)
    {
        if (userId != GetUserId(HttpContext)) return Forbid();
        CourseDto course = await userService.GetEnrolledCourseAsync(userId, courseId);
        return Ok(course);
    }

    [HttpPost("enrollments/{courseId}")]
    public async Task<IActionResult> Enroll(Guid userId, Guid courseId)
    {
        if (userId != GetUserId(HttpContext)) return Forbid();
        CourseDto course = await userService.CreateEnrollmentAsync(userId, courseId);
        return Created(string.Empty, course);
    }

    [HttpDelete("enrollments/{courseId}")]
    public async Task<IActionResult> Unenroll(Guid userId, Guid courseId)
    {
        if (userId != GetUserId(HttpContext)) return Forbid();
        await userService.DeleteEnrollmentAsync(userId, courseId);
        return NoContent();
    }
}