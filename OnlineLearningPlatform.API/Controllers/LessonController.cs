using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.API.Controllers;

public class LessonController : ApiControllerBase
{
    // [Authorize]
    [HttpPost("{lessonId}/progress")]
    public async Task<IActionResult> AddProgress(Guid lessonId, [FromBody] ProgressDto progressDto)
    {
        return Ok(progressDto);
    }

    // [Authorize]
    [HttpPost("{courseId}")]
    public async Task<IActionResult> AddLesson(Guid courseId, [FromBody] LessonDto lessonDto)
    {
        return Ok(lessonDto);
    }

    // [Authorize]
    [HttpDelete("{lessonId}")]
    public async Task<IActionResult> DeleteLesson(Guid lessonId)
    {
        return Ok(lessonId);
    }

    // [Authorize]
    [HttpPatch("{lessonId}")]
    public async Task<IActionResult> UpdateLesson(Guid lessonId, [FromBody] LessonDto lessonDto)
    {
        return Ok(lessonDto);
    }
}

