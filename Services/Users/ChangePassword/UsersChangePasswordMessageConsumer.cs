namespace Services.Users.ChangePassword;

using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;
using Services.Users.Authenticate;
using Services.Users.Mailing;
using Services.Users.Mailing.PutUserChangePasswordMailInQueue;
using SlimMessageBus;

public class UsersChangePasswordMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository,
    IMessageBus messageBus)
    : MessageConsumer<UsersChangePasswordMessage>(logger)
{
    protected override async Task DoOnHandle(UsersChangePasswordMessage message)
    {
        User user = (await usersRepository.GetByEmailConfirmationToken(message.Token))!;

        user.PasswordSalt = PasswordUtils.CreatePasswordSalt();
        user.PasswordHash = PasswordUtils.CreatePasswordHash(message.Password, user.PasswordSalt);

        user.EmailConfirmationToken = null;
        
        await usersRepository.Update(user);
        
        await messageBus.Publish(new PutUserChangePasswordMailInQueueMessage
        {
            UserId = user.Id,
        });
    }
}