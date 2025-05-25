using FluentValidation;
using UserManagement.Features.UserManagement.Commands;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Features.UserManagement.Validators;

public class CreateUserValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Username)
            .MustAsync(async (username, cancellation) =>
            {
                var existingUser = await userRepository.GetUserByQueryAsync(x => 
                    x.Username == username);
                return existingUser == null;
            }).WithMessage("Username already exists.");

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) =>
            {
                var existingUser = await userRepository.GetUserByQueryAsync(x =>
                    x.Email == email);
                return existingUser == null;
            }).WithMessage("Email already exists.");
    }
}
