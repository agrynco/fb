using Microsoft.AspNetCore.Authorization;
using Services.Users;

namespace Web.API.Controllers;

[Authorize]
public class AuthenticatedController : ApiControllerBase
{
    protected int GetAuthorizedUserId()
    {
        return Convert.ToInt32(User.FindFirst(JwtUtils.ID)!.Value);
    }
}