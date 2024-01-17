using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations;

public class CurrencyEntityConfiguration : EntityTypeConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(p => p.Name).IsUnique();

        builder.HasData(new Currency
        {
            Id = 1,
            Name = "Українська гривня",
            IsoCode = "UAH",
            SymbolOrAbbrev = "₴"
        }, new Currency
        {
            Id = 2,
            Name = "Євро",
            IsoCode = "EUR",
            SymbolOrAbbrev = "€"
        }, new Currency
        {
            Id = 3,
            Name = "Долар США",
            IsoCode = "USD",
            SymbolOrAbbrev = "$"
        });
    }
}