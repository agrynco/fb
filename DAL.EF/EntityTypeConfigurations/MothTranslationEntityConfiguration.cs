namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MothTranslationEntityConfiguration : EntityTypeConfiguration<MonthTranslation>
{
    public override void Configure(EntityTypeBuilder<MonthTranslation> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Value).IsRequired().HasMaxLength(25);

        builder.HasIndex(e => new
        {
            e.LanguageId,
            e.MonthNumber
        }).IsUnique();

        builder.HasIndex(e => new
        {
            e.LanguageId,
            e.Value,
            e.MonthNumber
        });

        const int UKRAINIAN_LANGUAGE_ID = 1;
        const int ENGLISH_LANGUAGE_ID = 2;

        builder.HasData(new MonthTranslation
            {
                Id = 1,
                MonthNumber = 1,
                Value = "Січень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 2,
                MonthNumber = 2,
                Value = "Лютий",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 3,
                MonthNumber = 3,
                Value = "Березень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 4,
                MonthNumber = 4,
                Value = "Квітень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 5,
                MonthNumber = 5,
                Value = "Травень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 6,
                MonthNumber = 6,
                Value = "Червень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 7,
                MonthNumber = 7,
                Value = "Липень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 8,
                MonthNumber = 8,
                Value = "Серпень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 9,
                MonthNumber = 9,
                Value = "Вересень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 10,
                MonthNumber = 10,
                Value = "Жовтень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 11,
                MonthNumber = 11,
                Value = "Листопад",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 12,
                MonthNumber = 12,
                Value = "Грудень",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },

            // English translations
            new MonthTranslation
            {
                Id = 13,
                MonthNumber = 1,
                Value = "January",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 14,
                MonthNumber = 2,
                Value = "February",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 15,
                MonthNumber = 3,
                Value = "March",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 16,
                MonthNumber = 4,
                Value = "April",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 17,
                MonthNumber = 5,
                Value = "May",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 18,
                MonthNumber = 6,
                Value = "June",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 19,
                MonthNumber = 7,
                Value = "July",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 20,
                MonthNumber = 8,
                Value = "August",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 21,
                MonthNumber = 9,
                Value = "September",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 22,
                MonthNumber = 10,
                Value = "October",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 23,
                MonthNumber = 11,
                Value = "November",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new MonthTranslation
            {
                Id = 24,
                MonthNumber = 12,
                Value = "December",
                LanguageId = ENGLISH_LANGUAGE_ID
            });
    }
}