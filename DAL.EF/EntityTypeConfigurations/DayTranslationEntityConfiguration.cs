namespace DAL.EF.EntityTypeConfigurations;

using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DayTranslationEntityConfiguration : EntityTypeConfiguration<DayTranslation>
{
    public override void Configure(EntityTypeBuilder<DayTranslation> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Value).IsRequired().HasMaxLength(25);

        builder.HasIndex(e => new
        {
            e.LanguageId,
            e.DayNumber
        }).IsUnique();

        builder.HasIndex(e => new
        {
            e.LanguageId,
            e.Value,
            e.DayNumber
        });

        const int UKRAINIAN_LANGUAGE_ID = 1;
        const int ENGLISH_LANGUAGE_ID = 2;

        builder.HasData( // Ukrainian translations
            new DayTranslation
            {
                Id = 1,
                DayNumber = 1,
                Value = "Понеділок",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 2,
                DayNumber = 2,
                Value = "Вівторок",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 3,
                DayNumber = 3,
                Value = "Середа",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 4,
                DayNumber = 4,
                Value = "Четвер",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 5,
                DayNumber = 5,
                Value = "П'ятниця",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 6,
                DayNumber = 6,
                Value = "Субота",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 7,
                DayNumber = 7,
                Value = "Неділя",
                LanguageId = UKRAINIAN_LANGUAGE_ID
            },

            // English translations
            new DayTranslation
            {
                Id = 8,
                DayNumber = 1,
                Value = "Monday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 9,
                DayNumber = 2,
                Value = "Tuesday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 10,
                DayNumber = 3,
                Value = "Wednesday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 11,
                DayNumber = 4,
                Value = "Thursday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 12,
                DayNumber = 5,
                Value = "Friday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 13,
                DayNumber = 6,
                Value = "Saturday",
                LanguageId = ENGLISH_LANGUAGE_ID
            },
            new DayTranslation
            {
                Id = 14,
                DayNumber = 7,
                Value = "Sunday",
                LanguageId = ENGLISH_LANGUAGE_ID
            });
    }
}