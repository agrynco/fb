namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEntityConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Email).HasMaxLength(250).IsRequired();
        builder.HasIndex(p => p.Email).IsUnique();

        builder.Property(p => p.FirstName).HasMaxLength(75);
        builder.Property(p => p.LastName).HasMaxLength(75);

        builder.Property(p => p.PasswordHash).HasMaxLength(250).IsRequired();
        builder.Property(p => p.PasswordSalt).HasMaxLength(250).IsRequired();

        builder.Property(p => p.Username).HasMaxLength(250).IsRequired();
        builder.HasIndex(p => p.Username).IsUnique();

        builder.HasData(new User
        {
            Id = 1,
            FirstName = "Anatolii",
            LastName = "Hrynchuk",
            Username = "agrynco",
            Email = "agrynco@gmail.com",
            Activated = true,
            PasswordHash = "6QqKKDVyflm2tQyTo2OxDp2WOjmPldJxyIV7HNsJeE0=",
            PasswordSalt = new byte[]
            {
                0xC3, 0xD3, 0x33, 0x41, 0x06, 0x61, 0xE0, 0x1D, 0x58, 0x0C, 0x16, 0x4C, 0x74, 0x8C, 0xF9, 0xD6
            },
            EmailConfirmationToken = "7bdcabb7-d216-45f6-b523-1ba6894899e3"
        }, new User
        {
            Id = 2,
            FirstName = "Family",
            LastName = "Budget",
            Username = "family.budget",
            Email = "family.budget.2023@gmail.com",
            Activated = true,
            PasswordHash = "6QqKKDVyflm2tQyTo2OxDp2WOjmPldJxyIV7HNsJeE0=",
            PasswordSalt = new byte[]
            {
                0xC3, 0xD3, 0x33, 0x41, 0x06, 0x61, 0xE0, 0x1D, 0x58, 0x0C, 0x16, 0x4C, 0x74, 0x8C, 0xF9, 0xD6
            },
            EmailConfirmationToken = "bf5f3882-cf29-49ea-bd2e-ebedb6ac0d1e"
        }, new User
        {
            Id = 3,
            FirstName = "Fbtest",
            LastName = "Fbtest",
            Username = "test",
            Email = "fb-test@i.ua",
            PasswordHash = "e2ammRNIGDeuxRu2h+pIHXAuT4tRmcaGYwlofzmEe8I=",
            Activated = true,
            PasswordSalt = new byte[]
            {
                0xCB, 0x23, 0xC6, 0x7B, 0xD1, 0x35, 0x7C, 0x8E, 0x43, 0xB5, 0x12, 0x48, 0x6C, 0x69, 0x84, 0x21
            },
            EmailConfirmationToken = "642584EE-0DCF-4D3A-A485-0AFCF81B00C8"
        });
    }
}