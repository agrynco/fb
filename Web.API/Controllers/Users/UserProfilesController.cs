namespace Web.API.Controllers.Users;

using Microsoft.AspNetCore.Mvc;
using Services.Users.GetProfile;
using Services.Users.UpdateFullName;
using Services.Users.UpdateUsername;
using SlimMessageBus;
using Web.API.Models.Users;
using Web.API.Models.Users.UpdateFullName;

[Route("users/profile")]
public class UserProfilesController(IMessageBus messageBus) : AuthenticatedController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        return Ok(await messageBus.Send(new UsersGetProfileRequest
        {
            UserId = GetAuthorizedUserId()
        }));
    }

    [HttpPut]
    [Route("full-name")]
    public async Task<IActionResult> UpdateFullName(UsersUpdateFullNameInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new UsersUpdateFullNameMessage
        {
            UserId = GetAuthorizedUserId(),
            FirstName = input.FirstName,
            LastName = input.LastName
        });

        return Ok();
    }

    [HttpPut]
    [Route("username")]
    public async Task<IActionResult> UpdateUsername(UsersUpdateUsernameInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new UsersUpdateUsernameMessage
        {
            UserId = GetAuthorizedUserId(),
            Username = input.Username
        });

        return Ok();
    }
}