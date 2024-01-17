namespace Services.Accounts.Delete;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record AccountsDeleteMessage : ITransactional
{
    public int AuthorizedUserId { get; init; }
    public required int[] Ids { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}