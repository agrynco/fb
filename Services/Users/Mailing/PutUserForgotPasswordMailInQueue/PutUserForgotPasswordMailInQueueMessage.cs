namespace Services.Users.Mailing.PutUserForgotPasswordMailInQueue;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record PutUserForgotPasswordMailInQueueMessage : ITransactional
{
    public required int UserId { get; init; }
    public IDbContextTransaction? DbContextTransaction { get; set; }
}