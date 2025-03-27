using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.CourseManagement;
using OnlineLearningPlatform.Application.Services.LessonManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
public class LessonController(ILessonService lessonService) : ApiControllerBase
{
    private readonly ILessonService lessonService = lessonService;

    [HttpPost("add-progress")]
    public async Task<IActionResult> AddProgress([FromBody] ProgressDto progressDto)
    {
        ProgressDto progress = await lessonService.AddProgressAsync(progressDto);
        return Created(string.Empty, progress);
    }

    [HttpPost("{courseId}")]
    public async Task<IActionResult> AddLesson(Guid courseId, [FromBody] LessonDto lessonDto)
    {
        LessonDto lesson = await lessonService.AddLessonAsync(GetUserId(HttpContext), lessonDto);
        return Created(string.Empty, lesson);
    }

    [HttpDelete("{lessonId}")]
    public async Task<IActionResult> DeleteLesson(Guid lessonId)
    {
        await lessonService.DeleteLessonAsync(GetUserId(HttpContext), lessonId);
        return NoContent();
    }

    [HttpPatch("{lessonId}")]
    public async Task<IActionResult> UpdateLesson([FromBody] LessonDto lessonDto)
    {
        LessonDto lesson = await lessonService.UpdateLessonAsync(GetUserId(HttpContext), lessonDto);
        return Ok(lesson);
    }
}

