using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;

namespace UserManagement.Infrastructure.Extensions;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.JoinDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

        #region init data
        builder.HasData(new User
        {
            Id = Guid.Parse("01971fe9-5030-7517-b000-a5a7b1368270"),
            Username = "user1",
            Email = "user1@example.com",
            Status = UserStatusEnum.Draft,
            JoinDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedDate = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc)
        });
        builder.HasData(new User
        {
            Id = Guid.Parse("01971fe9-fb80-7a02-84c7-8e6f604b3f86"),
            Username = "user2",
            Email = "user2@example.com",
            Status = UserStatusEnum.Active,
            JoinDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedDate = new DateTime(2025, 5, 2, 0, 0, 0, DateTimeKind.Utc)
        });
        builder.HasData(new User
        {
            Id = Guid.Parse("01971fea-1cb7-7d2f-b391-4107b22812a8"),
            Username = "user3",
            Email = "user3@example.com",
            Status = UserStatusEnum.Active,
            JoinDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedDate = new DateTime(2025, 5, 3, 0, 0, 0, DateTimeKind.Utc)
        });
        builder.HasData(new User
        {
            Id = Guid.Parse("01971fea-37cc-7bcc-8071-ecad077d0a53"),
            Username = "user4",
            Email = "user4@example.com",
            Status = UserStatusEnum.Active,
            JoinDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedDate = new DateTime(2025, 5, 4, 0, 0, 0, DateTimeKind.Utc)
        });
        builder.HasData(new User
        {
            Id = Guid.Parse("01971fea-555d-7c5e-ac91-8138324bb6a0"),
            Username = "user5",
            Email = "user5@example.com",
            Status = UserStatusEnum.Active,
            JoinDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedDate = new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc)
        });

        #endregion
    }
}
