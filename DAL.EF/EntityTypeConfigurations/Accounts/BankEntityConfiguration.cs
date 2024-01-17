using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations.Accounts;

public class BankEntityConfiguration : EntityTypeConfiguration<Bank>
{
    public override void Configure(EntityTypeBuilder<Bank> builder)
    {
        base.Configure(builder);

        builder.ToTable("Banks");

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(p => new
        {
            p.OwnerId,
            p.Name
        }).IsUnique();

        builder.HasData(new Bank
            {
                Id = 1,
                Name = "Приват банк",
                OwnerId = 1,
            },
            new Bank
            {
                Id = 2,
                Name = "MONO банк",
                OwnerId = 1
            },
            new Bank
            {
                Id = 3,
                Name = "Приват банк",
                OwnerId = 3,
            },
            new Bank
            {
                Id = 4,
                Name = "MONO банк",
                OwnerId = 3
            });
    }
}