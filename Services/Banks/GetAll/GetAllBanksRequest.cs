namespace Services.Banks.GetAll;

using DevExtreme.AspNet.Mvc;
using SlimMessageBus;

public record GetAllBanksRequest : IRequest<object>
{
    public DataSourceLoadOptions LoadOptions { get; init; } = default!;
    public int OwnerId { get; init; }
}