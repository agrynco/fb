namespace Services.Users.Mailing;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public class PutUserRegisterMailInQueueMessage : ITransactional
{
    public required int UserId { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}