using Domain.Finances;

namespace DAL.Abstract.Accounts;

public record AccountsGetByOwnerItem : AccountsGetByOwnerItemBase
{
    public AccountType Type { get; set; }
}