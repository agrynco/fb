namespace Services.Accounts.ResetVerified;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record AccountsToggleVerifiedMessage : ITransactional
{
    public int AccountId { get; set; }
    public int AuthorizedUserId { get; set; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}