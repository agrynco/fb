namespace Services.Users.RemoveEmailConfirmationToken;

public record RemoveEmailConfirmationTokenMessage
{
    public required int EmailId { get; init; }
}