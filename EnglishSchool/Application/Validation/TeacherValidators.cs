using Application.DTOs.Teacher;
using FluentValidation;

namespace Application.Validation;

public class CreateTeacherDtoValidator : AbstractValidator<CreateTeacherDto>
{
    public CreateTeacherDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(255).WithMessage("Subject must not exceed 255 characters");

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Experience years must be non-negative")
            .When(x => x.ExperienceYears.HasValue);
    }
}

public class UpdateTeacherDtoValidator : AbstractValidator<UpdateTeacherDto>
{
    public UpdateTeacherDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(255).WithMessage("Subject must not exceed 255 characters");

        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Experience years must be non-negative")
            .When(x => x.ExperienceYears.HasValue);
    }
}
