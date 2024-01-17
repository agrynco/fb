using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations.Accounts;

public class BankAccountEntityConfiguration : EntityTypeConfiguration<BankAccount>
{
    public override void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("BankAccounts");
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.IBAN).HasMaxLength(34);

        builder.HasIndex(p => new
        {
            p.BankId,
            p.IBAN
        }).IsUnique();
        
        builder.HasIndex(p => new
        {
            p.Name,
            p.OwnerId,
            p.CurrencyId
        }).IsUnique();

        builder.HasData(new BankAccount
        {
            Id = 9,
            CurrencyId = 2,
            BankId = 2,
            OwnerId = 1,
            Name = "ФОП",
            IBAN = "UA223220010000026005330066082"
        });
    }    
}