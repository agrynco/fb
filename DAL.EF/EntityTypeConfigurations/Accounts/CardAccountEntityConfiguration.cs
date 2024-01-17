using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations.Accounts;

public class CardAccountEntityConfiguration : EntityTypeConfiguration<CardAccount>
{
    public override void Configure(EntityTypeBuilder<CardAccount> builder)
    {
        base.Configure(builder);

        builder.ToTable("CardAccounts");
        builder.Property(p => p.CardNumber).IsRequired().HasMaxLength(16);
        
        builder.HasIndex(p => new
        {
            p.CardNumber,
            p.BankId
        }).IsUnique();

        builder.HasData(new CardAccount
            {
                Id = 4,
                CurrencyId = 1,
                OwnerId = 1,
                Name = "Карта ключ до рахунку",
                CardNumber = "5169330523068218",
                BankId = 1
            },
            new CardAccount
            {
                Id = 5,
                CurrencyId = 1,
                OwnerId = 1,
                Name = "Звичайна картка",
                CardNumber = "5375411412366993",
                BankId = 1
            },
            new CardAccount
            {
                Id = 6,
                CurrencyId = 1,
                OwnerId = 1,
                Name = "Чорна картка",
                CardNumber = "5375414144891016",
                BankId = 2
            },
            new CardAccount
            {
                Id = 7,
                CurrencyId = 1,
                OwnerId = 1,
                Name = "Біла картка",
                CardNumber = "537541508625303",
                BankId = 2
            });
    }
}