namespace DotnetTakeoff.Api.Application.Users.CreateUser;

internal static class CreateUser
{
    public static RouteGroupBuilder MapCreateUser(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", (UserModel user) => TypedResults.Ok())
            .WithName("CreateUser")
            .WithSummary("Create a new user");

        return group;
    }
}
