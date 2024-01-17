namespace Services.Accounts.GetById;

using Domain.Finances;

public record AccountsGetByIdResponse: AccountsGetByIdResponseBase
{
    public AccountType Type { get; set; }
}