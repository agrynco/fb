namespace Services.Banks.Delete;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record BanksDeleteMessage : ITransactional
{
    public int[] Ids { get; init; } = Array.Empty<int>();
    public int AuthorizedUserId { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}