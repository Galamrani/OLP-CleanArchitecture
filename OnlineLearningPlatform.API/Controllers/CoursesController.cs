using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.CourseManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
[Route("api/v1/courses")]
public class CoursesController(ICourseService courseService) : ApiControllerBase
{
    private readonly ICourseService courseService = courseService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourses()
    {
        List<CourseDto> courses = await courseService.GetCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{courseId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourse(Guid courseId)
    {
        CourseDto course = await courseService.GetCourseAsync(courseId);
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CourseDto courseDto)
    {
        CourseDto course = await courseService.AddCourseAsync(courseDto);
        return CreatedAtAction(nameof(GetCourse), new { courseId = course.Id }, course);
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> Delete(Guid courseId)
    {
        Guid userId = GetUserId(HttpContext);
        await courseService.DeleteCourseAsync(userId, courseId);
        return NoContent();
    }

    [HttpPatch("{courseId}")]
    public async Task<IActionResult> Update([FromBody] CourseDto courseDto)
    {
        Guid userId = GetUserId(HttpContext);
        CourseDto course = await courseService.UpdateCourseAsync(userId, courseDto);
        return Ok(course);
    }
}