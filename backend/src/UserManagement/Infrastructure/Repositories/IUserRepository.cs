using System.Linq.Expressions;
using UserManagement.Domain.Entities;
using UserManagement.Features.UserManagement.Commands;
using UserManagement.Features.UserManagement.DTOs;
using UserManagement.Features.UserManagement.Queries;

namespace UserManagement.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<PaginatedResult<User>> GetUsersAsync(GetUsersListQuery query);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByQueryAsync(Expression<Func<User, bool>> query);
    Task<User?> GetUserAsync(Guid Id);
    Task<Guid> AddUserAsync(User user);
    Task EditUserAsync(User user);
}
