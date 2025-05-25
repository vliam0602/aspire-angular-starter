using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.AppDbContexts;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Test.Infrastructure.Repositories;

public class UserRepositoryTest
{
    private readonly UserDbContext _dbContext;
    private readonly UserRepository _userRepository;
    private readonly IFixture _fixture = new Fixture();

    public UserRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "UserManagementTestDb")
            .Options;
        _dbContext = new UserDbContext(options);
        _dbContext.Database.EnsureCreated();
        
        // Reset the database before each test
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.SaveChanges();

        _userRepository = new UserRepository(_dbContext);
    }

    [Theory]
    [InlineData(5, 1, 3, 3)]
    [InlineData(5, 2, 3, 2)]
    public async Task GetUsersAsync_ShouldReturnPaginatedResult_WhenUsersExist(
        int itemsCount, int pageIndex, int pageSize, int expectedItemsEachPage
        )
    {
        // Arrange
        var users = _fixture.CreateMany<User>(itemsCount).ToList();
        await _dbContext.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();

        // Action
        var result = await _userRepository.GetUsersAsync
            (
                pageIndex: pageIndex,
                pageSize: pageSize
            );

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(expectedItemsEachPage);
        result.TotalCount.Should().Be(itemsCount);
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturnPaginatedWithSearchedResult_WhenSearchNotNull()
    {
        // Arrange
        var i = 0;
        var users = _fixture.Build<User>()
                            .With(u => u.Username, () => $"defaultUser-{i++}")
                            .CreateMany(3)
                            .ToList();

        users.AddRange(_fixture.CreateMany<User>(5));

        await _dbContext.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();

        // Action
        var result = await _userRepository.GetUsersAsync
            (
                pageIndex: 1,
                pageSize: 5,
                search: "defaultUser"
            );

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(3);
        result.TotalCount.Should().Be(3);
    }

}
