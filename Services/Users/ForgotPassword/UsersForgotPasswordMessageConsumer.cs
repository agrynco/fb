namespace Services.Users.ForgotPassword;

using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;
using Services.Users.Mailing.PutUserForgotPasswordMailInQueue;
using SlimMessageBus;

public class UsersForgotPasswordMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository,
    IMessageBus messageBus)
    : MessageConsumer<UsersForgotPasswordMessage>(logger)
{
    protected override async Task DoOnHandle(UsersForgotPasswordMessage message)
    {
        User? user = await usersRepository.GetByEmail(message.Email);

        if (user == null)
        {
            throw new ThereIsNoUserWithSuchEmailException(message.Email, message.IpAddress);
        }

        user.EmailConfirmationToken = Guid.NewGuid().ToString();

        await messageBus.Publish(new PutUserForgotPasswordMailInQueueMessage
        {
            UserId = user.Id,
        });
    }
}