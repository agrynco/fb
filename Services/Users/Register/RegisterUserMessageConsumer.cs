namespace Services.Users.Register;

using DAL.Abstract.Core;
using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;
using Services.RabbitMq.Core;
using Services.Users.Authenticate;
using Services.Users.Mailing;
using SlimMessageBus;

public class RegisterUserMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork,
    TemplateSettings templateSettings)
    : MessageConsumer<RegisterUserMessage>(logger)
{
    protected override async Task DoOnHandle(RegisterUserMessage message)
    {
        User user =
            await new Func<Task<User>>(async () =>
            {
                byte[] passwordSalt = PasswordUtils.CreatePasswordSalt();

                var newUser = new User
                {
                    Activated = false,
                    Email = message.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHash = PasswordUtils.CreatePasswordHash(message.Password, passwordSalt),
                    Username = message.Username,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    EmailConfirmationToken = Guid.NewGuid().ToString(),
                };

                await usersRepository.Add(newUser);

                return newUser;
            }).ExecuteInTransactionAsync(unitOfWork, message);

        await messageBus.Publish(new PutUserRegisterMailInQueueMessage
        {
            UserId = user.Id,
            DbContextTransaction = message.DbContextTransaction,
        });
    }
}