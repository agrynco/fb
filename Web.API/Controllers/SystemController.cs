using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

using System.Reflection;

[AllowAnonymous]
public class SystemController : ApiControllerBase
{
    [HttpGet]
    [Route("environment")]
    public string? GetEnvironment()
    {
        return ApplicationEnvironment.GetEnvironment();
    }

    [HttpGet]
    [Route("version")]
    public string Version()
    {
        Version version = Assembly.GetAssembly(GetType())!.GetName().Version!;
        return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}