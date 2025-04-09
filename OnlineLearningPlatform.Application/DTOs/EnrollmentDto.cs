using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.DTOs;

public record EnrollmentDto(
    Guid? Id,
    Guid UserId,
    Guid CourseId,
    DateTime? EnrolledAt,
    Course? Course
);