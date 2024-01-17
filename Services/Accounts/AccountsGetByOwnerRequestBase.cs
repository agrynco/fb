namespace Services.Accounts;

using DevExtreme.AspNet.Mvc;
using SlimMessageBus;

public record AccountsGetByOwnerRequestBase : IRequest<object>
{
    public int OwnerId { get; init; }
    public DataSourceLoadOptions LoadOptions { get; init; } = null!;
}