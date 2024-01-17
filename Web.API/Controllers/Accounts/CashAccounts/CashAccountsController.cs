namespace Web.API.Controllers.Accounts.CashAccounts;

using AutoMapper;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts.CashAccounts.Create;
using Services.Accounts.CashAccounts.GetById;
using Services.Accounts.CashAccounts.GetByOwner;
using Services.Accounts.Update;
using SlimMessageBus;
using Web.API.Models.Accounts;

[Route("accounts/cash")]
public class CashAccountsController(
        IMessageBus messageBus,
        IMapper mapper)
    : AuthenticatedController
{
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetAccounts(DataSourceLoadOptions loadOptions)
    {
        object result = await messageBus.Send(new CashAccountsGetByOwnerRequest
        {
            OwnerId = GetAuthorizedUserId(),
            LoadOptions = loadOptions
        });

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        object result = await messageBus.Send(new CashAccountsGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        });

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, CashAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<CashAccountsUpdateMessage>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        accountsUpdateMessage.Id = id;

        await messageBus.Publish(accountsUpdateMessage);
        return Ok();
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(CashAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<CashAccountsCreateRequest>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        
        return Ok(await messageBus.Send(accountsUpdateMessage));
    }
}