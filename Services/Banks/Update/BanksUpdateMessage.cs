namespace Services.Banks.Update;

public record BanksUpdateMessage
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int AuthorizedUserId { get; init; }
}