namespace Services.Users.ConfirmEmail;

using DAL.Abstract;
using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;
using Services.Users.RemoveEmailConfirmationToken;
using SlimMessageBus;

public class UsersConfirmEmailMessageConsumer(
        ILogger logger,
        IUsersRepository usersRepository,
        IMessageBus messageBus,
        IOutgoingEmailsRepository outgoingEmailsRepository)
    : MessageConsumer<UsersEmailConfirmationMessage>(logger)
{
    protected override async Task DoOnHandle(UsersEmailConfirmationMessage message)
    {
        User? user = await usersRepository.GetByEmailConfirmationToken(message.EmailConfirmationToken);

        if (user == null)
        {
            throw new WrongEmailConfirmationTokenException(message.EmailConfirmationToken);
        }
        
        user.Activated = true;
        user.EmailConfirmationToken = null;
        
        await usersRepository.Update(user);
    }
}