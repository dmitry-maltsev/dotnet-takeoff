using FluentValidation;

namespace DotnetTakeoff.Api.Application.Users.CreateUser;

public record UserModel(
    string FirstName,
    string LastName,
    string Email);

internal class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(5);
        RuleFor(x => x.LastName).NotEmpty();
    }
}
