namespace Services.Users.UpdateFullName;

public record UsersUpdateFullNameMessage
{
    public required int UserId { get; init; }
    public required string FirstName { get; init; } = default!;
    public required string LastName { get; init; } = default!;
}