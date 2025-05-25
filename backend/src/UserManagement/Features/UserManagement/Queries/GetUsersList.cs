using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Features.UserManagement.DTOs;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Features.UserManagement.Queries;

public record GetUsersListQuery(

    string? Search,
    string? SortColumn,
    string? SortOrder,
    string? FilterColumn,
    string? FilterKey,
    int PageIndex = 1,
    int PageSize = 5

) : IRequest<PaginatedResult<User>>;

public class GetUsersListHandler(
    IUserRepository userRepository
) : IRequestHandler<GetUsersListQuery, PaginatedResult<User>>
{
    public async Task<PaginatedResult<User>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUsersAsync
            (
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                search: request.Search
            );
    }
}
