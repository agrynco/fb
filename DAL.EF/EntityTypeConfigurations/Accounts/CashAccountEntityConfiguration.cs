using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations.Accounts;

public class CashAccountEntityConfiguration : EntityTypeConfiguration<CashAccount>
{
    public override void Configure(EntityTypeBuilder<CashAccount> builder)
    {
        base.Configure(builder);

        builder.ToTable("CashAccounts");

        builder.HasData(new CashAccount
            {
                Id = 1,
                CurrencyId = 1,
                OwnerId = 1,
                Name = "Накопичення",
                Balance = 85160
            },
            new CashAccount
            {
                Id = 2,
                CurrencyId = 2,
                OwnerId = 1,
                Name = "Накопичення"
            },
            new CashAccount
            {
                Id = 3,
                CurrencyId = 3,
                OwnerId = 1,
                Name = "Накопичення"
            }, new CashAccount
            {
                Id = 10,
                CurrencyId = 1,
                OwnerId = 3,
                Name = "Накопичення",
                Balance = 85160
            },
            new CashAccount
            {
                Id = 11,
                CurrencyId = 2,
                OwnerId = 3,
                Name = "Накопичення"
            },
            new CashAccount
            {
                Id = 12,
                CurrencyId = 3,
                OwnerId = 3,
                Name = "Накопичення"
            },
            new CashAccount
            {
                Id = 13,
                CurrencyId = 3,
                OwnerId = 1,
                Name = "Closed account",
                Closed = true
            }
        );
    }
}