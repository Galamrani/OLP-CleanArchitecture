using FluentValidation;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Validators;


public class CourseDtoValidator : AbstractValidator<CourseDto>
{
    public CourseDtoValidator()
    {
        // Title is required and must be between 5 and 200 characters
        RuleFor(course => course.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(5, 200).WithMessage("Title must be between 5 and 200 characters");

        // Description is optional but should not exceed 2000 characters
        RuleFor(course => course.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        // CreatorId is required
        RuleFor(course => course.CreatorId)
            .NotEmpty().WithMessage("CreatorId is required");
    }
}
