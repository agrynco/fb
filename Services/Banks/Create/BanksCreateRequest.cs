namespace Services.Banks.Create;

public record BanksCreateRequest : ICreateRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int AuthorizedUserId { get; init; }
}