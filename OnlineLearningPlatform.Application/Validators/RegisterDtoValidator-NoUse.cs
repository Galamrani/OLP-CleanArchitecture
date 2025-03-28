using FluentValidation;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        // Name is required and must be between 2 and 100 characters
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");

        // Email is required and must be a valid email format
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        // Password must be:
        // At least 8 characters
        // Contain at least one uppercase letter
        // Contain at least one digit
        // Contain at least one non-alphanumeric character (special char)
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one digit")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (non-alphanumeric)");
    }
}
