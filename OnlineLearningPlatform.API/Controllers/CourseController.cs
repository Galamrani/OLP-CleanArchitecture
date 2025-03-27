using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.CourseManagement;

namespace OnlineLearningPlatform.API.Controllers;

// [Authorize]
public class CourseController(ICourseService courseService) : ApiControllerBase
{
    private readonly ICourseService courseService = courseService;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        List<CourseDto> courses = await courseService.GetCoursesAsync();
        return Ok(courses);
    }

    [AllowAnonymous]
    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetBasicCourse(Guid courseId)
    {
        // Retrieves a basic course including its lessons Only! and NO! user's progress.
        CourseDto course = await courseService.GetBasicCourseAsync(courseId);
        return Ok(course);
    }

    [HttpGet("full-course/{courseId}")]
    public async Task<IActionResult> GetFullCourse(Guid courseId)
    {
        // Retrieves a full course including its lessons and the current user's progress.
        CourseDto course = await courseService.GetFullCourseAsync(GetUserId(HttpContext), courseId);
        return Ok(course);
    }

    [HttpGet("student/my-courses")]
    public async Task<IActionResult> GetEnrolledCourses()
    {
        List<CourseDto> courses = await courseService.GetUserEnrolledCoursesAsync(GetUserId(HttpContext));
        return Ok(courses);
    }

    [HttpGet("instructor/my-courses")]
    public async Task<IActionResult> GetCreatedCourses()
    {
        List<CourseDto> courses = await courseService.GetUserCreatedCoursesAsync(GetUserId(HttpContext));
        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] CourseDto courseDto)
    {
        return Ok(courseDto);
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteCourse(Guid courseId)
    {
        return Ok(courseId);
    }

    [HttpPatch("{courseId}")]
    public async Task<IActionResult> UpdateCourse(Guid courseId, [FromBody] CourseDto courseDto)
    {
        return Ok(courseDto);
    }

    [HttpPost("{courseId}/enroll")]
    public async Task<IActionResult> EnrollToCourse(Guid courseId)
    {
        return Ok(courseId);
    }

    [HttpDelete("{courseId}/unenroll")]
    public async Task<IActionResult> UnenrollToCourse(Guid courseId)
    {
        return Ok(courseId);
    }
}