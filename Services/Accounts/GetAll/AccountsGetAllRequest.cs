namespace Services.Accounts.GetAll;

using DAL.Abstract.Accounts;
using SlimMessageBus;

public record AccountsGetAllRequest : IRequest<AccountDto[]>
{
    public int AuthorizedUserId { get; init; }
}