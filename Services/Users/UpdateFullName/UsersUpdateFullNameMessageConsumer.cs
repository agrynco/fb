namespace Services.Users.UpdateFullName;

using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;

public class UsersUpdateFullNameMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository) 
    : MessageConsumer<UsersUpdateFullNameMessage>(logger)
{
    protected override async Task DoOnHandle(UsersUpdateFullNameMessage message)
    {
        User user = await usersRepository.GetById(message.UserId);
        
        user.FirstName = message.FirstName;
        user.LastName = message.LastName;
        
        await usersRepository.Update(user);
    }
}