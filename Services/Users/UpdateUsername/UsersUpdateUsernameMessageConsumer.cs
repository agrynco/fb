namespace Services.Users.UpdateUsername;

using DAL.Abstract.Users;
using Domain;
using Serilog;
using Services.Core;

public class UsersUpdateUsernameMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository) 
    : MessageConsumer<UsersUpdateUsernameMessage>(logger)
{
    public IUsersRepository UsersRepository { get; } = usersRepository;

    protected override async Task DoOnHandle(UsersUpdateUsernameMessage message)
    {
        User user = await usersRepository.GetById(message.UserId);
        
        user.Username = message.Username;
        
        await usersRepository.Update(user);
    }
}