namespace Web.API.Controllers.FamilyMembers;

using DAL.Abstract.FamilyMembers;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Services.FamilyMembers.Create;
using Services.FamilyMembers.Delete;
using Services.FamilyMembers.GetById;
using Services.FamilyMembers.GetByOwner;
using Services.FamilyMembers.GetByOwnerForAutocomplete;
using Services.FamilyMembers.Update;
using SlimMessageBus;
using Web.API.Models;
using Web.API.Models.FamilyMembers;

/// <summary>
///     Controller for managing family members.
/// </summary>
[Route("family-members")]
public class FamilyMembersController(IMessageBus messageBus) : AuthenticatedController
{
    /// <summary>
    ///     Deletes the specified family members.
    /// </summary>
    /// <param name="ids">The IDs of the family members to delete.</param>
    /// <returns>An <see cref="StatusCodes.Status200OK" /></returns>
    /// <remarks>
    ///     This method sends a message to the message bus to delete the specified family members.
    ///     It requires the IDs of the family members to delete and the authorized user ID.
    ///     The message is published to the message bus asynchronously.
    /// </remarks>
    [HttpDelete]
    [Route("")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int[] ids)
    {
        await messageBus.Publish(new FamilyMembersDeleteMessage
        {
            Ids = ids,
            AuthorizedUserId = GetAuthorizedUserId()
        });

        return Ok();
    }

    /// <summary>
    ///     Retrieves family members by owner request.
    /// </summary>
    /// <param name="loadOptions">The data source load options.</param>
    /// <note type="DataSourceLoadOptions">
    ///     <see cref="DataSourceLoadOptions" />
    /// </note>
    /// <returns>An IActionResult representing the result of the request.</returns>
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
    {
        return Ok(await messageBus.Send(new FamilyMembersGetByOwnerRequest
        {
            LoadOptions = loadOptions,
            AuthorizedUserId = GetAuthorizedUserId()
        }));
    }
    
    [HttpGet]
    [Route("my/autocomplete")]
    public async Task<IActionResult> Get()
    {
        return Ok(await messageBus.Send(new FamilyMembersGetByOwnerForAutocompleteRequest
        {
            AuthorizedUserId = GetAuthorizedUserId()
        }));
    }

    /// <summary>
    ///     Retrieves a family member by their ID.
    /// </summary>
    /// <param name="id">The ID of the family member to retrieve.</param>
    /// <returns>
    ///     <see cref="FamilyMemberDto" />
    /// </returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(FamilyMemberDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await messageBus.Send(new FamilyMembersGetByIdRequest
        {
            Id = id,
            AuthorizedUserId = GetAuthorizedUserId()
        }));
    }

    /// <summary>
    ///     Updates a family member with the given ID.
    /// </summary>
    /// <param name="id">The ID of the family member to update.</param>
    /// <param name="input">The updated information for the family member.</param>
    /// <returns>
    ///     - 200 OK if the operation was successful.
    ///     - 400 Bad Request if the input is invalid or the model state is not valid.
    /// </returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, FamilyMemberCreateOrUpdateModel input)
    {
        if (ModelState.IsValid)
        {
            await messageBus.Publish(new FamilyMembersUpdateMessage
            {
                AuthorizedUserId = GetAuthorizedUserId(),
                Id = id,
                Name = input.Name
            });
            return Ok();
        }

        return BadRequest(ModelState.ToRequestError());
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(FamilyMemberCreateOrUpdateModel input)
    {
        if (ModelState.IsValid)
        {
            await messageBus.Send(new FamilyMembersCreateRequest
            {
                AuthorizedUserId = GetAuthorizedUserId(),
                Name = input.Name
            });
            return Ok();
        }

        return BadRequest(ModelState.ToRequestError());
    }
}