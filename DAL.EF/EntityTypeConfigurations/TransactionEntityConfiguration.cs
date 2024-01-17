using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations;

public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("Transactions");
        builder.Property(p => p.Description).HasMaxLength(500);
        

        var transactions = new[]
        {
            new Transaction
            {
                Id = 1,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 1, 0, 1, 0),
                Amount = 100000,
                Description = "Income",
            },
            new Transaction
            {
                Id = 2,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 1, 10, 0, 0),
                Amount = -2400,
                Description = "Продукти",
                CategoryId = 2
            },
            new Transaction
            {
                Id = 3,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 7, 11, 0, 0),
                Amount = -200,
                Description = "Інтернет",
                CategoryId = 19
            },
            new Transaction
            {
                Id = 4,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 15, 14, 30, 0),
                Amount = -440,
                Description = "Шашлик",
                CategoryId = 4
            },
            new Transaction
            {
                Id = 5,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 15, 14, 30, 0),
                Amount = -1450,
                Description = "Газ",
                CategoryId = 50
            },
            new Transaction
            {
                Id = 6,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 28, 14, 30, 0),
                Amount = -350,
                Description = "Холодна вода",
                CategoryId = 17
            },
            new Transaction
            {
                Id = 7,
                AccountId = 1,
                ActorId = 1,
                TransactionDateTime = new DateTime(2023, 5, 29, 10, 15, 0),
                Amount = -1000,
                Description = "Консерви",
                CategoryId = 22
            }
        };

        builder.HasData(transactions);
    }
}