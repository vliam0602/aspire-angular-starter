using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.Domain.Entities;
using UserManagement.Features.UserManagement.Commands;
using UserManagement.Features.UserManagement.DTOs;
using UserManagement.Features.UserManagement.Queries;
using UserManagement.Infrastructure.AppDbContexts;

namespace UserManagement.Infrastructure.Repositories;

public class UserRepository(
     UserDbContext _dbContext
) : IUserRepository
{
    public async Task<Guid> AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task EditUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByQueryAsync(Expression<Func<User, bool>> query)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(query);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<PaginatedResult<User>> GetUsersAsync(GetUsersListQuery query)
    {
        var users = _dbContext.Users.AsQueryable();

        // search
        if (!string.IsNullOrEmpty(query.Search))
        {
            var searchkey = query.Search.Trim().ToLower();
            users = users.Where(u => u.Username.ToLower().Contains(searchkey) 
            || u.Email.ToLower().Contains(searchkey));
        }
        // filter
        // sort
        // pagination
        var totalCount = await users.CountAsync();
        var result = await users  
            .Skip((query.PageIndex - 1) * query.PageSize)  // remember to check pagIndex >1
            .Take(query.PageSize)
            .ToListAsync();

        return new PaginatedResult<User>
        (
            items:  result,
            totalCount: totalCount,
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );
    }
    

}
