namespace Services.Users.Mailing;

using Common;
using DAL.Abstract;
using DAL.Abstract.Core;
using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;
using Services.RabbitMq;
using Services.RabbitMq.Core;

public class PutUserRegisterMailInQueueMessageConsumer(
    ILogger logger,
    IOutgoingEmailTemplatesRepository outgoingEmailTemplatesRepository,
    IOutgoingEmailsRepository outgoingEmailsRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    TemplateSettings templateSettings,
    IClock clock,
    IRabbitMqService rabbitMqService)
    : MessageConsumer<PutUserRegisterMailInQueueMessage>(logger)
{
    protected override async Task DoOnHandle(PutUserRegisterMailInQueueMessage message)
    {
        OutgoingEmailTemplate template =
            (await outgoingEmailTemplatesRepository.GetByType(EmailTemplateType.UserActivation))!;

        User user = (await usersRepository.GetById(message.UserId))!;

        var templateModel = new
        {
            user.FirstName,
            user.LastName,
            templateSettings.SiteName,
            templateSettings.SiteUrl,
            user.EmailConfirmationToken
        };

        var outgoingEmail = new OutgoingEmail
        {
            Body = OutgoingEmailTemplateProcessor.Apply(template.BodyTemplate, templateModel),
            Subject = OutgoingEmailTemplateProcessor.Apply(template.SubjectTemplate, templateModel),
            UserId = message.UserId,
            Status = EmailSendingStatus.Pending,
            TemplateId = template.Id,
            ValidUntil = clock.UtcNow.AddHours(24)
        };

        await new Func<Task>(async () => { await outgoingEmailsRepository.Add(outgoingEmail); })
            .ExecuteInTransactionAsync(unitOfWork, message);

        try
        {
            rabbitMqService.SendMessage(MessageQueues.UserRegistered, new SendUserRegisteredMailMessageData
            {
                Subject = outgoingEmail.Subject,
                Body = outgoingEmail.Body,
                To = user.Email,
                EmailId = outgoingEmail.Id,
                UserId = user.Id
            });
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while sending email data to RabbitMQ");
        }
    }
}