using FluentValidation;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Validators;

public class EnrollmentDtoValidator : AbstractValidator<EnrollmentDto>
{
    public EnrollmentDtoValidator()
    {
        // UserId is required
        RuleFor(e => e.UserId)
            .NotEmpty().WithMessage("UserId is required");

        // CourseId is required
        RuleFor(e => e.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
    }
}
