namespace Services.Accounts.CashAccounts.Create;

using SlimMessageBus;

public record CashAccountsCreateRequest: IRequest<int>
{
    public int AuthorizedUserId { get; set; }
    public int CurrencyId { get; set; }
    public string? Description { get; set; }
    public required string Name { get; set; } = default!;
}