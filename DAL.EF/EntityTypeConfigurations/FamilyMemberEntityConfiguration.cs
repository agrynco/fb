namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FamilyMemberEntityConfiguration : EntityTypeConfiguration<FamilyMember>
{
    public override void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        
        builder.HasIndex(p => new
        {
            p.Name,
            p.OwnerId
        }).IsUnique();

        builder.HasData(new FamilyMember
            {
                Id = 1,
                OwnerId = 3,
                Name = "Мама"
            },
            new FamilyMember
            {
                Id = 2,
                OwnerId = 3,
                Name = "Тато"
            },
            new FamilyMember
            {
                Id = 3,
                OwnerId = 3,
                Name = "Я"
            },
            new FamilyMember
            {
                Id = 4,
                OwnerId = 3,
                Name = "Брат"
            },
            new FamilyMember
            {
                Id = 5,
                OwnerId = 3,
                Name = "Сестра"
            },
            new FamilyMember
            {
                Id = 6,
                OwnerId = 3,
                Name = "Дідусь"
            },
            new FamilyMember
            {
                Id = 7,
                OwnerId = 3,
                Name = "Бабуся"
            },
            new FamilyMember
            {
                Id = 8,
                OwnerId = 3,
                Name = "Дядько"
            },
            new FamilyMember
            {
                Id = 9,
                OwnerId = 3,
                Name = "Тітка"
            },
            new FamilyMember
            {
                Id = 10,
                OwnerId = 3,
                Name = "Кузен"
            },
            new FamilyMember
            {
                Id = 11,
                OwnerId = 3,
                Name = "Кузина"
            },
            new FamilyMember
            {
                Id = 12,
                OwnerId = 3,
                Name = "Племінник"
            },
            new FamilyMember
            {
                Id = 13,
                OwnerId = 3,
                Name = "Племінниця"
            },
            new FamilyMember
            {
                Id = 14,
                OwnerId = 3,
                Name = "Двоюрідний брат"
            },
            new FamilyMember
            {
                Id = 15,
                OwnerId = 3,
                Name = "Двоюрідна сестра"
            },
            new FamilyMember
            {
                Id = 16,
                OwnerId = 3,
                Name = "Двоюрідний дідусь"
            },
            new FamilyMember
            {
                Id = 17,
                OwnerId = 3,
                Name = "Двоюрідна бабуся"
            },
            new FamilyMember
            {
                Id = 18,
                OwnerId = 1,
                Name = "Test 1"
            },
            new FamilyMember
            {
                Id = 19,
                OwnerId = 1,
                Name = "Test 2"
            });
    }
}