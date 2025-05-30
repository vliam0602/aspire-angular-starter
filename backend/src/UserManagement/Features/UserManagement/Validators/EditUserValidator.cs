using FluentValidation;
using System.ComponentModel;
using UserManagement.Domain.Enums;
using UserManagement.Features.UserManagement.Commands;

namespace UserManagement.Features.UserManagement.Validators;

public class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status is required.")
            .Must(status => Enum.IsDefined(typeof(UserStatusEnum), status))
            .WithMessage("Invalid status value.");
    }
}
