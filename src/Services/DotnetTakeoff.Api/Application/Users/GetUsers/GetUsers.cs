using DotnetTakeoff.Api.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DotnetTakeoff.Api.Application.Users.GetUsers;

internal static class GetUsers
{
    public static RouteGroupBuilder MapGetUsers(this RouteGroupBuilder group)
    {
        group
            .MapGet("/", Handler)
            .WithName("GetUsers")
            .WithSummary("Get all users")
            .Produces<UserDetails[]>();

        return group;
    }

    public static async ValueTask<IResult> Handler(DotnetTakeoffContext dotnetTakeoff, CancellationToken ct)
    {
        var users = await dotnetTakeoff.Users
            .Select(user => UserDetails.FromUser(user))
            .ToListAsync(ct);

        return TypedResults.Ok(users);
    }
}
