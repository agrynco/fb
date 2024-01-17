namespace Services.Accounts;

using SlimMessageBus;

public record AccountsGetByIdRequestBase<TResponse> : IRequest<TResponse>
    where TResponse : AccountsGetByIdResponseBase
{
    public int Id { get; init; }
    public int OwnerId { get; init; }
}