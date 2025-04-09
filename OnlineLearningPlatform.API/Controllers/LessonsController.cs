using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Services.LessonManagement;

namespace OnlineLearningPlatform.API.Controllers;

[Authorize]
[Route("api/v1/courses/{courseId}/lessons")]
public class LessonsController(ILessonService lessonService) : ApiControllerBase
{
    private readonly ILessonService lessonService = lessonService;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] LessonDto lessonDto)
    {
        Guid userId = GetUserId(HttpContext);
        LessonDto lesson = await lessonService.AddLessonAsync(userId, lessonDto);
        return Created(string.Empty, lesson);
    }

    [HttpDelete("{lessonId}")]
    public async Task<IActionResult> Delete(Guid lessonId)
    {
        Guid userId = GetUserId(HttpContext);
        await lessonService.DeleteLessonAsync(userId, lessonId);
        return NoContent();
    }

    [HttpPatch("{lessonId}")]
    public async Task<IActionResult> Update([FromBody] LessonDto lessonDto)
    {
        Guid userId = GetUserId(HttpContext);
        LessonDto lesson = await lessonService.UpdateLessonAsync(userId, lessonDto);
        return Ok(lesson);
    }

    [HttpPost("{lessonId}/progress")]
    public async Task<IActionResult> AddProgress([FromBody] ProgressDto progressDto)
    {
        ProgressDto progress = await lessonService.AddLessonProgressAsync(progressDto);
        return Created(string.Empty, progress);
    }
}

