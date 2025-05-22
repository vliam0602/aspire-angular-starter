using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure;

public static class DIRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
