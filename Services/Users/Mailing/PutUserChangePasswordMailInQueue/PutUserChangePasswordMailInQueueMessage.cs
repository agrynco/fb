namespace Services.Users.Mailing.PutUserChangePasswordMailInQueue;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record PutUserChangePasswordMailInQueueMessage : ITransactional
{
    public required int UserId { get; set; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}