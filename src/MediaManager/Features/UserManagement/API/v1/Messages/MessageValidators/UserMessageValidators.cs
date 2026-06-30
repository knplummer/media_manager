using FluentValidation;
using MediaManager.Features.UserManagement.Abstractions.Models;

namespace MediaManager.Features.UserManagement.API.v1.Messages.MessageValidators;

public abstract class BaseUserValidator<T> : AbstractValidator<T> where T : UserRecord
{
    public abstract void AdditionalRules();
    public BaseUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.");

        AdditionalRules();
    }
}

public class CreateUserValidator : BaseUserValidator<CreateUserMessage>
{
    public override void AdditionalRules()
    {
        RuleFor(x => x.Username)
            .MaximumLength(25).WithMessage("Username cannot exceed 25 characters.");
    }

}

public class UpdateUserValidator : BaseUserValidator<UpdateUserMessage>
{
    public override void AdditionalRules()
    {
        //No additional rules required for update user
    }

}