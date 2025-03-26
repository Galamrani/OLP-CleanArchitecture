namespace OnlineLearningPlatform.Application.DTOs;

public record LessonDto(
    Guid? Id,
    Guid CourseId,
    string Title,
    string? Description,
    string VideoUrl,
    ICollection<ProgressDto>? Progresses
);