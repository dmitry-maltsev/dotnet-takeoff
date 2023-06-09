namespace DotnetTakeoff.Api.Application.Entities;

public class User
{
    public long Id { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLogin { get; set; }
}
