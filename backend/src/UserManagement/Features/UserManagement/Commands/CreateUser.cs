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
        var user = new User
        {
            Username = command.Username,
            Email = command.Email
        };
        return await userRepository.AddUserAsync(user);
    }
}


