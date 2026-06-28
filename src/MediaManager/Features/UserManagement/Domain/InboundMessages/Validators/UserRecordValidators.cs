using FluentValidation;
using MediaManager.Features.UserManagement.Domain.Abstractions;
using MediaManager.Features.UserManagement.Domain.InboundMessages;

namespace MediaManager.Features.UserManagement.Domain.InboundMessages.Validators;

public class BaseUserValidator<T> : AbstractValidator<T> where T : UserRecord
{
    public BaseUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.");
    }
}

public class CreateUserValidator : BaseUserValidator<CreateUserMessage>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");
    }
}

public class UpdateUserValidator : BaseUserValidator<UpdateUserMessage>
{
}