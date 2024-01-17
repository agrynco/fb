namespace Services.FamilyMembers.Delete;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record FamilyMembersDeleteMessage : ITransactional
{
    public int AuthorizedUserId { get; init; }
    public int[] Ids { get; init; } = Array.Empty<int>();
    public IDbContextTransaction? DbContextTransaction { get; set; }
}