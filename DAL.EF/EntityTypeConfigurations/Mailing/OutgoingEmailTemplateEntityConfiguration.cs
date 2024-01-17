namespace DAL.EF.EntityTypeConfigurations.Mailing;

using Common;
using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OutgoingEmailTemplateEntityConfiguration : EntityTypeConfiguration<OutgoingEmailTemplate>
{
    public override void Configure(EntityTypeBuilder<OutgoingEmailTemplate> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.SubjectTemplate).IsRequired().HasMaxLength(300);
        builder.HasIndex(p => p.SubjectTemplate).IsUnique();

        builder.Property(p => p.BodyTemplate).IsRequired();

        builder.HasData(new OutgoingEmailTemplate
        {
            Id = 1,
            SubjectTemplate = "Підтвердження реєстрації",
            Type = EmailTemplateType.UserActivation,
            BodyTemplate = ResourceReader.ReadAsString(GetType(),
                "DAL.EF.EntityTypeConfigurations.Mailing.ConfirmationRegistration.html")
        },
        new OutgoingEmailTemplate
        {
            Id = 2,
            SubjectTemplate = "Скидання паролю",
            Type = EmailTemplateType.ForgotPassword,
            BodyTemplate = ResourceReader.ReadAsString(GetType(),
                "DAL.EF.EntityTypeConfigurations.Mailing.ResetPassword.html")
        },
        new OutgoingEmailTemplate
        {
            Id = 3,
            SubjectTemplate = "Пароль змінено",
            Type = EmailTemplateType.ThePasswordHasBeenChanged,
            BodyTemplate = ResourceReader.ReadAsString(GetType(),
                "DAL.EF.EntityTypeConfigurations.Mailing.ThePasswordHasBeenChanged.html")
        });
    }
}