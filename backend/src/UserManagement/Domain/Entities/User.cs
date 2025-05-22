using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = default!;

    [EmailAddress]
    public string Email { get; set; } = default!;

    public UserStatusEnum Status { get; set; } = UserStatusEnum.Draft;

    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
}
