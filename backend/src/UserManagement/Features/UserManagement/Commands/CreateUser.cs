using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Features.UserManagement.Commands;

public record CreateUserCommand(
    string Username,
    string Email
) : IRequest<Guid>;

public class CreateUserHandler(
    IUserRepository userRepository
) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(command.Email);
        if (existingUser != null)
        {
            throw new ArgumentException($"User with email {command.Email} already exists.");
        }
        var user = new User
        {
            Username = command.Username,
            Email = command.Email
        };
        return await userRepository.AddUserAsync(user);
    }
}


