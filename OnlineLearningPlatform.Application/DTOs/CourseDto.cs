namespace OnlineLearningPlatform.Application.DTOs;

public record CourseDto(
    Guid Id,
    Guid CreatorId,
    string Title,
    string? Description,
    DateTime CreatedAt,
    ICollection<LessonDto>? Lessons
);