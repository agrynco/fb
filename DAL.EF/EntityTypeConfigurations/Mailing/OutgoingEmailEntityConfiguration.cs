namespace DAL.EF.EntityTypeConfigurations.Mailing;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OutgoingEmailEntityConfiguration : EntityTypeConfiguration<OutgoingEmail>
{
    public override void Configure(EntityTypeBuilder<OutgoingEmail> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Subject).IsRequired().HasMaxLength(400);
        builder.Property(p => p.Body).IsRequired();
    }
}