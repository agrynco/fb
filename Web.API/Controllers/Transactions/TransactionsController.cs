namespace Web.API.Controllers.Transactions;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Accounts;
using Services.Accounts.GetById;
using Services.Transactions.Create;
using Services.Transactions.Delete;
using Services.Transactions.Get;
using Services.Transactions.GetAccountId;
using Services.Transactions.GetForCreateOrEdit;
using Services.Transactions.SetConfirmation;
using Services.Transactions.Update;
using SlimMessageBus;
using Web.API.Models;
using Web.API.Models.Transactions;

public class TransactionsController(
    IMessageBus messageBus,
    IMapper mapper) : AuthenticatedController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get(
        TransactionsDataSourceLoadOptionsModel loadOptions, [FromQuery] string languageKey)
    {
        object? transactionsGetResponse = await messageBus.Send(new TransactionsGetRequest
        {
            LoadOptions = loadOptions,
            ActorId = GetAuthorizedUserId(),
            LanguageKey = languageKey
        });

        return Ok(transactionsGetResponse);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetForEdit(int id)
    {
        return Ok(await messageBus.Send(new TransactionsGetForEditRequest
        {
            Id = id,
            AuthorizedUserId = GetAuthorizedUserId()
        }));
    }

    [HttpPut]
    [Route("{id}/confirmation")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetConfirmation(int id, TransactionSetConfirmationModel input)
    {
        await messageBus.Publish(new TransactionsSetConfirmationMessage
        {
            Id = id,
            ActorId = GetAuthorizedUserId(),
            Confirmed = input.Confirmed
        });

        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TransactionCreateOrUpdateModel input)
    {
        await ValidateAccountsOwnership(new[]
        {
            input.SourceAccountId
        }, GetAuthorizedUserId());

        if (input.DestinationAccountId.HasValue)
        {
            await ValidateAccountsOwnership(new[]
            {
                input.DestinationAccountId.Value
            }, GetAuthorizedUserId());
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var request = mapper.Map<TransactionUpdateMessage>(input);
        request.ActorId = GetAuthorizedUserId();
        request.Id = id;

        await messageBus.Publish(request);

        return Ok();
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(TransactionCreateOrUpdateModel input)
    {
        await ValidateAccountsOwnership([
            input.SourceAccountId
        ], GetAuthorizedUserId());

        if (input.DestinationAccountId.HasValue)
        {
            await ValidateAccountsOwnership(new[]
            {
                input.DestinationAccountId.Value
            }, GetAuthorizedUserId());
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var request = mapper.Map<TransactionsCreateRequest>(input);
        request.ActorId = GetAuthorizedUserId();

        return Ok(await messageBus.Send(request));
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> Delete([FromQuery] int[] ids)
    {
        if (ids.Length == 0)
        {
            return BadRequest(new RequestError(
                new RequestErrorItem
                {
                    Key = nameof(ids),
                    ErrorMessage = "Are required"
                }));
        }

        await ValidateAccountsOwnership(await messageBus.Send(new GetAccountIdsByTransactionIdsRequest
        {
            TransactionIds = ids
        }), GetAuthorizedUserId());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        var request = new TransactionsDeleteMessage
        {
            Ids = ids,
            ActorId = GetAuthorizedUserId()
        };

        await messageBus.Publish(request);

        return Ok();
    }

    private async Task ValidateAccountsOwnership(int[] accountIds, int ownerId)
    {
        foreach (int accountId in accountIds)
        {
            try
            {
                await messageBus.Send(new AccountsGetByIdRequest
                {
                    Id = accountId,
                    OwnerId = ownerId
                });
            }
            catch (WrongAccountsOwnerException ex)
            {
                ModelState.AddModelError(nameof(accountId), ex.Message);
            }
        }
    }
}