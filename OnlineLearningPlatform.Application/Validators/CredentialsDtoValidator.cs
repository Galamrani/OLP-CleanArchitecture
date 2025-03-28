using FluentValidation;
using OnlineLearningPlatform.Application.DTOs;

namespace OnlineLearningPlatform.Application.Validators;

public class CredentialsDtoValidator : AbstractValidator<CredentialsDto>
{
    public CredentialsDtoValidator()
    {
        // Email is required and must be a valid format
        RuleFor(credentials => credentials.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        // Password must be:
        // At least 8 characters
        // Contain at least one uppercase letter
        // Contain at least one digit
        // Contain at least one special character (non-alphanumeric)
        RuleFor(credentials => credentials.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one digit")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (non-alphanumeric)");
    }
}
