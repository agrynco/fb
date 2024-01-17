namespace DAL.EF.EntityTypeConfigurations.Accounts;

using Domain.Finances;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccountEntityConfiguration : EntityTypeConfiguration<Account>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.HasIndex(p => new
        {
            p.Name,
            p.OwnerId,
            p.CurrencyId,
            p.Type
        }).IsUnique();
    }
}