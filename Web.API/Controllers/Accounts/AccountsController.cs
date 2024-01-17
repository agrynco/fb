namespace Web.API.Controllers.Accounts;

using DAL.Abstract.Accounts;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts.Delete;
using Services.Accounts.GetAll;
using Services.Accounts.GetById;
using Services.Accounts.GetWideAll;
using Services.Accounts.GetWideById;
using Services.Accounts.ResetVerified;
using SlimMessageBus;

public class AccountsController(IMessageBus messageBus) : AuthenticatedController
{
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetMyAccounts(DataSourceLoadOptions loadOptions)
    {
        object result = await messageBus.Send(new AccountsGetByOwnerRequest
        {
            OwnerId = GetAuthorizedUserId(),
            LoadOptions = loadOptions
        });

        return Ok(result);
    }

    [HttpPut]
    [Route("my/{id}/toggle-verified")]
    public async Task<IActionResult> ToggleVerified(int id)
    {
        await messageBus.Publish(new AccountsToggleVerifiedMessage
        {
            AccountId = id,
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok();
    }

    [HttpGet]
    [Route("my/all")]
    public async Task<IActionResult> GetAll()
    {
        object result = await messageBus.Send(new AccountsGetAllRequest
        {
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok(result);
    }

    [HttpGet]
    [Route("my/wide")]
    public async Task<IActionResult> GetMyAccountsWide(DataSourceLoadOptions loadOptions)
    {
        object result = await messageBus.Send(new AccountsGetWideByOwnerRequest
        {
            AuthorizedUserId = GetAuthorizedUserId(),
            LoadOptions = loadOptions
        });

        return Ok(result);
    }

    [HttpGet]
    [Route("my/wide/{id}")]
    public async Task<IActionResult> GetWide(int id)
    {
        WideAccountDto wideAccountDto = await messageBus.Send(new AccountsGetWideByIdRequest
        {
            Id = id,
            AuthenticatedUserId = GetAuthorizedUserId()
        });

        return Ok(wideAccountDto);
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> Delete([FromQuery] int[] ids)
    {
        try
        {
            await messageBus.Publish(new AccountsDeleteMessage
            {
                Ids = ids,
                AuthorizedUserId = GetAuthorizedUserId()
            });
        }
        catch (AccountsDeleteException)
        {
            ModelState.AddModelError("ids", "Some accounts can contain transactions.");
            return BadRequest(ModelState.ToRequestError());
        }

        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(AccountsGetByIdResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForEdit(int id)
    {
        AccountsGetByIdResponse accountsGetByIdResponse = await messageBus.Send(new AccountsGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        });

        return Ok(accountsGetByIdResponse);
    }
}