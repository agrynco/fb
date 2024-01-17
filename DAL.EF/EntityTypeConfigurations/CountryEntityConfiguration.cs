using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain.Finances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations;

public class CountryEntityConfiguration : EntityTypeConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);
        
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(p => p.Name).IsUnique();

        builder.HasData(new Country
            {
                Id = 1,
                Name = "Австрія"
            },
            new Country
            {
                Id = 2,
                Name = "Бельгія"
            }, new Country
            {
                Id = 3,
                Name = "Болгарія"
            }, new Country
            {
                Id = 4,
                Name = "Венгрія"
            }, new Country
            {
                Id = 5,
                Name = "Німеччина"
            }, new Country
            {
                Id = 6,
                Name = "Греція"
            }, new Country
            {
                Id = 7,
                Name = "Данія"
            }, new Country
            {
                Id = 8,
                Name = "Ірландія"
            }, new Country
            {
                Id = 9,
                Name = "Іспанія"
            }, new Country
            {
                Id = 10,
                Name = "Італія"
            }, new Country
            {
                Id = 11,
                Name = "Кіпр"
            }, new Country
            {
                Id = 12,
                Name = "Латвія"
            }, new Country
            {
                Id = 13,
                Name = "Литва"
            }, new Country
            {
                Id = 14,
                Name = "Люксембург"
            }, new Country
            {
                Id = 15,
                Name = "Мальта"
            }, new Country
            {
                Id = 16,
                Name = "Нідерланди"
            }, new Country
            {
                Id = 17,
                Name = "Польща"
            }, new Country
            {
                Id = 18,
                Name = "Португалія"
            }, new Country
            {
                Id = 19,
                Name = "Руминія"
            }, new Country
            {
                Id = 20,
                Name = "Словакія"
            }, new Country
            {
                Id = 21,
                Name = "Словенія"
            }, new Country
            {
                Id = 22,
                Name = "Фінляндія"
            }, new Country
            {
                Id = 23,
                Name = "Франція"
            }, new Country
            {
                Id = 24,
                Name = "Хорватія"
            }, new Country
            {
                Id = 25,
                Name = "Чехія"
            }, new Country
            {
                Id = 26,
                Name = "Швеція"
            }, new Country
            {
                Id = 27,
                Name = "Естонія"
            },
            new Country
            {
                Id = 28,
                Name = "Україна"
            },
            new Country
            {
                Id = 29,
                Name = "США"
            });
    }
}