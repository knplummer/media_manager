using FluentValidation;

namespace MediaManager.Features.UserManagement.Records.Validators;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");
    }

}

public class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.");
    }

}