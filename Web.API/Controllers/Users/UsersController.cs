namespace Web.API.Controllers.Users;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Services.Core;
using Services.Users.Authenticate;
using Services.Users.Authenticate.Exceptions;
using Services.Users.ChangePassword;
using Services.Users.ConfirmEmail;
using Services.Users.ForgotPassword;
using Services.Users.GetAvatar;
using Services.Users.GetProfile;
using Services.Users.IsEmailBusy;
using Services.Users.IsUsernameBusy;
using Services.Users.Register;
using Services.Users.SetAvatar;
using Services.Users.UpdateFullName;
using Services.Users.UpdateUsername;
using SlimMessageBus;
using Web.API.Controllers.Users.ConfigSections;
using Web.API.Exceptions;
using Web.API.Models;
using Web.API.Models.Users;
using Web.API.Models.Users.Authenticate;
using Web.API.Models.Users.ChangePassword;
using Web.API.Models.Users.ForgotPassword;
using Web.API.Models.Users.Register;
using Web.API.Models.Users.UpdateFullName;

public class UsersController(
    IMessageBus messageBus,
    JwtConfigSection jwtConfigSection,
    IMapper mapper) : AuthenticatedController
{
    /// <summary>
    /// Retrieves the IP address of the client.
    /// </summary>
    /// <returns>The IP address of the client as a string.</returns>
    /// <exception cref="CanNotDetectIpAddressException">Thrown when the IP address cannot be detected.</exception>
    private string GetIpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out StringValues address))
        {
            return address!;
        }

        if (HttpContext.Connection.RemoteIpAddress == null)
        {
            throw new CanNotDetectIpAddressException();
        }

        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="input"><see cref="RegisterUserInput"/></param>
    /// <returns><see cref="StatusCodes.Status200OK"/> or <see cref="StatusCodes.Status400BadRequest"/> with <see cref="RequestError"/> </returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterUserInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        if (await messageBus.Send(new IsUsernameBusyRequest
            {
                Username = input.Username
            }))
        {
            ModelState.AddModelError(nameof(RegisterUserInput.Username), "usernameIsBusy");
        }

        if (await CheckIsEmailAlreadyTaken(input))
        {
            ModelState.AddModelError(nameof(RegisterUserInput.Email), "emailIsBusy");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(mapper.Map<RegisterUserMessage>(input));
        return Ok();
    }

    /// <summary>
    /// ConfirmEmail method handles the request to confirm the user's email.
    /// </summary>
    /// <param name="emailConfirmationToken">The token used to confirm the email.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("confirm-email/{emailConfirmationToken}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail(string emailConfirmationToken)
    {
        try
        {
            await messageBus.Publish(new UsersEmailConfirmationMessage
            {
                EmailConfirmationToken = emailConfirmationToken
            });

            return Ok();
        }
        catch (MessageConsumerException ex) when (ex.InnerException is WrongEmailConfirmationTokenException)
        {
            return BadRequest(new RequestError(new RequestErrorItem
            {
                Key = nameof(emailConfirmationToken),
                ErrorMessage = "Wrong email confirmation token"
            }));
        }
    }

    /// <summary>
    /// Checks if the provided email is already taken.
    /// </summary>
    /// <param name="input">The RegisterUserInput object containing the email to be checked.</param>
    /// <returns>Returns a Task representing the asynchronous operation that returns true if the email is already taken, false otherwise.</returns>
    private async Task<bool> CheckIsEmailAlreadyTaken(RegisterUserInput input)
    {
        return await messageBus.Send(new IsEmailAlreadyTakenRequest
        {
            Email = input.Email
        });
    }

    /// <summary>
    /// Endpoint for user sign-in.
    /// </summary>
    /// <returns>Returns <see cref="AuthenticateUserOutput"/> object representing the result of the sign-in operation.</returns>
    [HttpPost]
    [Route("signin")]
    [ProducesResponseType(typeof(AuthenticateUserOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn()
    {
        AuthenticateUserResponse response = await messageBus.Send(new AuthenticateUserByTokenRequest
        {
            IpAddress = GetIpAddress(),
            RefreshTokenTtl = jwtConfigSection.RefreshTokenTtl,
            JwtKey = jwtConfigSection.Key,
            TokenLiveDuration = jwtConfigSection.TokenLiveDuration,
            UserId = GetAuthorizedUserId()
        });

        return Ok(new AuthenticateUserOutput
        {
            RefreshToken = response.RefreshToken,
            Email = response.Email,
            FirstName = response.FirstName,
            LastName = response.LastName,
            JwtToken = response.JwtToken,
            Id = response.Id
        });
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AuthenticateUserOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authenticate(AuthenticateUserInput userInput)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        try
        {
            AuthenticateUserResponse response = await messageBus.Send(
                new AuthenticateUserByCredentialsRequest
                {
                    Password = userInput.Password,
                    Username = userInput.Username,
                    JwtKey = jwtConfigSection.Key,
                    RefreshTokenTtl = jwtConfigSection.RefreshTokenTtl,
                    TokenLiveDuration = jwtConfigSection.TokenLiveDuration,
                    IpAddress = GetIpAddress()
                });

            return Ok(new AuthenticateUserOutput
            {
                RefreshToken = response.RefreshToken,
                Email = response.Email,
                FirstName = response.FirstName,
                LastName = response.LastName,
                JwtToken = response.JwtToken,
                Id = response.Id
            });
        }
        catch (AccountIsNotActivatedException)
        {
            return BadRequest(new RequestError(new RequestErrorItem
            {
                Key = nameof(AuthenticateUserInput.Username),
                ErrorMessage = "Your account is not activated"
            }));
        }
        catch (IncorrectPasswordException)
        {
            return BadRequest(new RequestError(new RequestErrorItem
            {
                Key = nameof(AuthenticateUserInput.Password),
                ErrorMessage = "Wrong password"
            }));
        }
        catch (IncorrectUsernameException)
        {
            return BadRequest(new RequestError(new RequestErrorItem
            {
                Key = nameof(AuthenticateUserInput.Username),
                ErrorMessage = "Incorrect username"
            }));
        }
    }

    /// <summary>
    /// The first step in Flow "Forgot Password". It should send an email with a link to the
    /// change password page. The link looks like {{SiteUrl}}/reset-password?token={{EmailConfirmationToken}}
    /// </summary>
    /// <param name="input"><see cref="ForgotPasswordInput"/></param>
    /// <returns><see cref="StatusCodes.Status200OK"/> or <see cref="RequestError"/></returns>
    [HttpPost]
    [Route("forgot-password")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new UsersForgotPasswordMessage
        {
            Email = input.Email,
            IpAddress = GetIpAddress()
        });

        return Ok();
    }

    /// <summary>
    /// The second step in Flow "Forgot Password". It should change the password and send an email to the user
    /// that password has been changed.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("change-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new UsersChangePasswordMessage
        {
            Password = input.Password,
            Token = input.Token
        });

        return Ok();
    }

    [HttpGet]
    [Route("avatar")]
    public async Task<IActionResult> GetAvatar()
    {
        UsersGetAvatarResponse usersGetAvatarResponse = await messageBus.Send(new UsersGetAvatarRequest
        {
            UserId = GetAuthorizedUserId()
        });

        if (usersGetAvatarResponse == null)
        {
            return NotFound();
        }

        return File(usersGetAvatarResponse.Data, usersGetAvatarResponse.ContentType);
    }

    [HttpPost]
    [Route("avatar")]
    public async Task<IActionResult> UpdateAvatar(IFormFile image)
    {
        if (image == null)
        {
            return BadRequest(new RequestError(new RequestErrorItem
            {
                Key = nameof(image),
                ErrorMessage = "Form.Files does not contain required data"
            }));
        }

        using (var stream = new MemoryStream())
        {
            await image.CopyToAsync(stream);
            byte[] fileBytes = stream.ToArray();

            await messageBus.Publish(new UsersSetAvatarMessage
            {
                UserId = GetAuthorizedUserId(),
                Data = fileBytes,
                ContentType = image.ContentType,
                Name = image.FileName
            });
        }

        return Ok();
    }

    
}