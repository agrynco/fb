namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FileEntityConfiguration : EntityTypeConfiguration<File>
{
    public override void Configure(EntityTypeBuilder<File> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Data).IsRequired();
        builder.Property(p => p.ContentType).IsRequired().HasMaxLength(10);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}