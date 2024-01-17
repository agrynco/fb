namespace Services.Users.RemoveEmailConfirmationToken;

using DAL.Abstract;
using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;

public class RemoveEmailConfirmationTokenMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository,
    IOutgoingEmailsRepository outgoingEmailsRepository) 
    : MessageConsumer<RemoveEmailConfirmationTokenMessage>(logger)
{
    protected override async Task DoOnHandle(RemoveEmailConfirmationTokenMessage message)
    {
        User user = await outgoingEmailsRepository.GetUserByMailId(message.EmailId);
        user.EmailConfirmationToken = null;

        await usersRepository.Update(user);
    }
}