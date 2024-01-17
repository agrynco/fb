namespace Web.API.Controllers.Accounts.BankAccounts;

using AutoMapper;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts.BankAccounts;
using Services.Accounts.BankAccounts.Create;
using Services.Accounts.BankAccounts.GetById;
using Services.Accounts.CardAccounts.Update;
using SlimMessageBus;
using Web.API.Models.Accounts;

[Route("accounts/bank")]
public class BankAccountsController(
        IMessageBus messageBus,
        IMapper mapper)
    : AuthenticatedController
{
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetMyAccounts(DataSourceLoadOptions loadOptions)
    {
        object result = await messageBus.Send(new BankAccountsGetByOwnerRequest
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
        object result = await messageBus.Send(new BankAccountsGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        });

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, BankAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<BankAccountsUpdateMessage>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        accountsUpdateMessage.Id = id;

        await messageBus.Publish(accountsUpdateMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(BankAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<BankAccountsCreateRequest>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        
        return Ok(await messageBus.Send(accountsUpdateMessage));
    }
}