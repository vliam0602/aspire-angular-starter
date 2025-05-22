using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Features.UserManagement.Queries;

public record GetUserDetailQuery(Guid Id) : IRequest<User>;

public class GetUserDetailHandler(
    IUserRepository userRepository
    ) : IRequestHandler<GetUserDetailQuery, User?>
{
    public async Task<User?> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserAsync(request.Id);
    }
}
