namespace OnlineLearningPlatform.Application.DTOs;

public record ProgressDto(
    Guid? Id,
    Guid UserId,
    Guid LessonId,
    DateTime? LastWatchedAt
);