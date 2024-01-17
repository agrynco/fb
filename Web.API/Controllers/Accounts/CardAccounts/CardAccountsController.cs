namespace Web.API.Controllers.Accounts.CardAccounts;

using AutoMapper;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts.CardAccounts.Create;
using Services.Accounts.CardAccounts.GetById;
using Services.Accounts.CardAccounts.GetByOwner;
using Services.Accounts.CardAccounts.Update;
using SlimMessageBus;
using Web.API.Models.Accounts;

[Route("accounts/card")]
public class CardAccountsController(IMessageBus messageBus, IMapper mapper): AuthenticatedController
{
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetMyAccounts(DataSourceLoadOptions loadOptions)
    {
        object result = await messageBus.Send(new CardAccountsGetByOwnerRequest
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
        object result = await messageBus.Send(new CardAccountsGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        });

        return Ok(result);
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, CardAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<CardAccountsUpdateMessage>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        accountsUpdateMessage.Id = id;

        await messageBus.Publish(accountsUpdateMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(CardAccountsCreateOrUpdateModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var accountsUpdateMessage = mapper.Map<CardAccountsCreateRequest>(input);
        accountsUpdateMessage.AuthorizedUserId = GetAuthorizedUserId();
        
        return Ok(await messageBus.Send(accountsUpdateMessage));
    }
}