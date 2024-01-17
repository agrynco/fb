namespace Services.Users.SetAvatar;

using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

public record UsersSetAvatarMessage : ITransactional
{
    public int UserId { get; init; }
    public required byte[] Data { get; init; } = default!;
    public required string ContentType { get; init; } = default!;
    public required string Name { get; init; } = default!;
    public IDbContextTransaction? DbContextTransaction { get; set; }
}