namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LanguageEntityConfiguration : EntityTypeConfiguration<Language>
{
    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Key).IsRequired().HasMaxLength(5);
        
        builder.HasIndex(p => new
        {
            p.Key
        }).IsUnique();

        builder.HasData(new Language
            {
                Id = 1,
                Key = "ua"
            },
            new Language
            {
                Id = 2,
                Key = "en"
            });
    }
}