using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.CourseManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
[Route("courses")]
public class CoursesController(ICourseService courseService) : ApiControllerBase
{
    private readonly ICourseService courseService = courseService;

    [HttpGet]
    [AllowAnonymous]
    // NOTE: fetch all courses - no lessons
    public async Task<IActionResult> GetCourses()
    {
        List<CourseDto> courses = await courseService.GetCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{courseId}")]
    [AllowAnonymous]
    // NOTE: fetch course with lessons and progresses if user have auth, else with only lessons
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
        await courseService.DeleteCourseAsync(GetUserId(HttpContext), courseId);
        return NoContent();
    }

    [HttpPatch("{courseId}")]
    public async Task<IActionResult> Update([FromBody] CourseDto courseDto)
    {
        CourseDto course = await courseService.UpdateCourseAsync(GetUserId(HttpContext), courseDto);
        return Ok(course);
    }

    [HttpPost("{courseId}/enrollments")]
    public async Task<IActionResult> Enroll(Guid courseId)
    {
        CourseDto course = await userService.CreateEnrollmentAsync(GetUserId(HttpContext), courseId);
        return Created(string.Empty, course);
    }

    [HttpDelete("{courseId}/enrollments")]
    public async Task<IActionResult> Unenroll(Guid courseId)
    {
        await userService.DeleteEnrollmentAsync(GetUserId(HttpContext), courseId);
        return NoContent();
    }
}