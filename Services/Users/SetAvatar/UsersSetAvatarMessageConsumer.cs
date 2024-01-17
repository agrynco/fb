namespace Services.Users.SetAvatar;

using DAL.Abstract.Core;
using DAL.Abstract.Files;
using DAL.Abstract.Users;
using Serilog;
using Services.Core;

public class UsersSetAvatarMessageConsumer(
    ILogger logger,
    IUsersRepository usersRepository,
    IFilesRepository filesRepository,
    IUnitOfWork unitOfWork)
    : MessageConsumer<UsersSetAvatarMessage>(logger)
{
    protected override async Task DoOnHandle(UsersSetAvatarMessage message)
    {
        var user = (await usersRepository.GetById(message.UserId))!;

        await new Func<Task>(async () =>
        {
            if (user.AvatarId != null)
            {
                await filesRepository.Remove((int)user.AvatarId);
            }

            int fileId = await filesRepository.Add(new Domain.File
            {
                Data = message.Data,
                ContentType = message.ContentType,
                Name = message.Name
            });
            
            user.AvatarId = fileId;
            
            await usersRepository.Update(user);
            
        }).ExecuteInTransactionAsync(unitOfWork, message);
    }
}