using FluentValidation;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Validators;

public class LessonDtoValidator : AbstractValidator<LessonDto>
{
    public LessonDtoValidator()
    {
        // CourseId is required (must be a valid GUID)
        RuleFor(lesson => lesson.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        // Title is required and must be between 5 and 200 characters
        RuleFor(lesson => lesson.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(5, 200).WithMessage("Title must be between 5 and 200 characters");

        // Description is optional but must not exceed 2000 characters
        RuleFor(lesson => lesson.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        // VideoUrl is required and must be a valid URL
        RuleFor(lesson => lesson.VideoUrl)
            .NotEmpty().WithMessage("Video URL is required")
            .Matches(@"^(https?:\/\/)?([\w\-]+(\.[\w\-]+)+)([\/?#][^\s]*)?$")
            .WithMessage("Invalid video URL format");
    }
}
