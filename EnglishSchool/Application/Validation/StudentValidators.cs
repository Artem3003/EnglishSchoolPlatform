using Application.DTOs.Student;
using FluentValidation;

namespace Application.Validation;

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.Level)
            .MaximumLength(50).WithMessage("Level must not exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Level));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past")
            .When(x => x.DateOfBirth.HasValue);
    }
}

public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Level)
            .MaximumLength(50).WithMessage("Level must not exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.Level));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past")
            .When(x => x.DateOfBirth.HasValue);
    }
}
