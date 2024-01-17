namespace Web.API.Controllers.Banks;

using System.Net;
using DevExtreme.AspNet.Mvc;
using Domain.Finances;
using Microsoft.AspNetCore.Mvc;
using Services.Banks.Create;
using Services.Banks.Delete;
using Services.Banks.GetAll;
using Services.Banks.GetById;
using Services.Banks.Update;
using SlimMessageBus;
using Web.API.Models;
using Web.API.Models.Banks;

public class BanksController(IMessageBus messageBus) : AuthenticatedController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
    {
        object getAllBanksResponse = await messageBus.Send(new GetAllBanksRequest
        {
            LoadOptions = loadOptions,
            OwnerId = GetAuthorizedUserId()
        });

        return Ok(getAllBanksResponse);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType<BanksGetByIdResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await messageBus.Send(new BanksGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        }));
    }

    /// <summary>
    /// Deletes multiply <see cref="Bank"/>s by <paramref name="ids"/>
    /// </summary>
    /// <param name="ids">Array of <see cref="Bank"/>'s ids</param>
    /// <returns><see cref="HttpStatusCode.OK"/> or <see cref="HttpStatusCode.BadRequest"/></returns>
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
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

        await messageBus.Publish(new BanksDeleteMessage
        {
            Ids = ids,
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await messageBus.Publish(new BanksDeleteMessage
        {
            Ids = new[]
            {
                id
            },
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok();
    }

    /// <summary>
    ///    Creates new <see cref="Bank" />
    /// </summary>
    /// <param name="input"><see cref="AddOrUpdateBankModel"/></param>
    /// <returns>Created Bank id or <see cref="RequestError"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(AddOrUpdateBankModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        return Ok(await messageBus.Send(new BanksCreateRequest
        {
            Name = input.Name,
            Description = input.Description,
            AuthorizedUserId = GetAuthorizedUserId(),
        }));
    }

    /// <summary>
    /// Updates <see cref="Bank"/> with <paramref name="id"/>
    /// </summary>
    /// <param name="id">Identifier of <see cref="Bank"/></param>
    /// <param name="input"><see cref="AddOrUpdateBankModel"/></param>
    /// <returns>Empty response or <see cref="RequestError"/> </returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] AddOrUpdateBankModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new BanksUpdateMessage
        {
            Id = id,
            Name = input.Name,
            Description = input.Description,
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok();
    }
}