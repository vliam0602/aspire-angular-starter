using MediatR;
using UserManagement.Domain.Enums;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Features.UserManagement.Commands;

public record EditUserCommand(
    Guid Id,
    string Username,
    int Status
) : IRequest<bool>;

public class EditUserHandler(
    IUserRepository userRepository
) : IRequestHandler<EditUserCommand, bool>
{
    public async Task<bool> Handle(EditUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserAsync(command.Id);
        if (user == null)
        {
            throw new ArgumentException($"User with id {command.Id} not found.");
        }

        // check if updating username already exists
        var existingUser = await userRepository.GetUserByQueryAsync(x =>
            x.Username.Equals(command.Username));

        if (existingUser != null && existingUser.Id != command.Id)
        {
            throw new ArgumentException(
                $"User with username \'{command.Username}\' already exists.");
        }

        // update user info
        user.Username = command.Username;
        user.Status = (UserStatusEnum)command.Status;

        await userRepository.EditUserAsync(user);
        return true;
    }
}
