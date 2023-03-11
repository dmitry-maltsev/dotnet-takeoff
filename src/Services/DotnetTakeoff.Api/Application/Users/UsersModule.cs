using DotnetTakeoff.Api.Application.Users.CreateUser;
using DotnetTakeoff.Api.Application.Users.GetUsers;
using DotnetTakeoff.Api.Extensions;

namespace DotnetTakeoff.Api.Application.Users;

internal static class UsersModule
{
    public static RouteGroupBuilder MapUsersRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users")
            .WithValidation()
            .WithTags("Users")
            .WithOpenApi();

        group.MapGetUsers();
        group.MapCreateUser();

        return group;
    }
}
