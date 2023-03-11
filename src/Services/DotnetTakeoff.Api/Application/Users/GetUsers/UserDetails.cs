using DotnetTakeoff.Api.Application.Entities;

namespace DotnetTakeoff.Api.Application.Users.GetUsers;

public record UserDetails(
    long Id,
    string? Email,
    string? FirstName,
    string? LastName,
    bool IsActive,
    DateTime? LastLogin
)
{
    public static UserDetails FromUser(User user) => new(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.IsActive,
        user.LastLogin
    );
}
