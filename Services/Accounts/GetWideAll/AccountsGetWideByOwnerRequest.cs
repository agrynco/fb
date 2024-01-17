namespace Services.Accounts.GetWideAll;

using DevExtreme.AspNet.Mvc;
using SlimMessageBus;

public record AccountsGetWideByOwnerRequest : IRequest<object>
{
    public required DataSourceLoadOptions LoadOptions { get; set; } 
    public required int AuthorizedUserId { get; init; }
}