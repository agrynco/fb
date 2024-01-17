using Microsoft.EntityFrameworkCore.Storage;
using Services.Transactions.Create.Transfer;

namespace Services.Users.Register;

public class RegisterUserMessage : ITransactional
{
    public required string Email { get; init; } = default!;

    public required string FirstName { get; init; } = default!;

    public required string LastName { get; init; } = default!;

    public required string Password { get; init; } = default!;

    public required string Username { get; init; } = default!;
    public IDbContextTransaction? DbContextTransaction { get; set; }
}