namespace Web.API.Controllers.TransactionCategories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.TransactionCategories.Add;
using Services.TransactionCategories.Add.Update;
using Services.TransactionCategories.ContainsTransactions;
using Services.TransactionCategories.Delete;
using Services.TransactionCategories.FindByName;
using Services.TransactionCategories.GetById;
using Services.TransactionCategories.GetByOwner;
using Services.TransactionCategories.IsExistsWithId;
using SlimMessageBus;
using Web.API.Models;
using Web.API.Models.TransactionCategories;

[Route("transaction-categories")]
public class TransactionCategoriesController(
        IMessageBus messageBus)
    : AuthenticatedController
{
    [HttpGet]
    [Route("my")]
    public async Task<IActionResult> GetMyTransactionCategories()
    {
        GetByOwnerTransactionCategoriesResponse getByOwnerTransactionCategoriesResponse =
            await messageBus.Send(new GetByOwnerTransactionCategoriesRequest
            {
                OwnerId = GetAuthorizedUserId()
            });

        return Ok(getByOwnerTransactionCategoriesResponse);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType<TransactionCategoriesGetByIdResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await messageBus.Send(new TransactionCategoriesGetByIdRequest
        {
            Id = id,
            OwnerId = GetAuthorizedUserId()
        }));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AddOrUpdateTransactionCategoryModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        int? alreadyExistedId = await messageBus.Send(new FindByNameRequest
        {
            Name = input.Name,
            OwnerId = GetAuthorizedUserId(),
            ParentId = input.ParentId
        });

        if (alreadyExistedId.HasValue && alreadyExistedId.Value != id)
        {
            ModelState.AddModelError(nameof(input.Name),
                $"Transaction category with {nameof(input.Name)} {input.Name} already exists.");
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new UpdateTransactionCategoryMessage
        {
            Description = input.Description,
            Id = id,
            Name = input.Name,
            OwnerId = GetAuthorizedUserId(),
            ParentId = input.ParentId
        });

        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddTransactionCategory(AddOrUpdateTransactionCategoryModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.ToRequestError());
        }

        if ((await messageBus.Send(new FindByNameRequest
            {
                Name = input.Name,
                OwnerId = GetAuthorizedUserId(),
                ParentId = input.ParentId
            })).HasValue)
        {
            ModelState.AddModelError(nameof(input.Name),
                $"Transaction category with {nameof(input.Name)} {input.Name} already exists.");

            return BadRequest(ModelState.ToRequestError());
        }

        int addTransactionCategoryResponse = await messageBus.Send(new AddTransactionCategoryRequest
        {
            Description = input.Description,
            Name = input.Name,
            OwnerId = GetAuthorizedUserId(),
            ParentId = input.ParentId
        });

        return Ok(addTransactionCategoryResponse);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RequestError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Delete(int id)
    {
        if ((await messageBus.Send(new GetByOwnerTransactionCategoriesRequest
            {
                OwnerId = GetAuthorizedUserId(),
                ParentId = id
            })).Items.Length != 0)
        {
            ModelState.AddModelError(nameof(id),
                $"Category with id {id} contains subcategories. Please, delete them first");

            return BadRequest(ModelState.ToRequestError());
        }

        if (!await messageBus.Send(new IsTransactionCategoryWithIdExistsRequest
            {
                Id = id,
                OwnerId = GetAuthorizedUserId()
            }))
        {
            ModelState.AddModelError(nameof(id), $"Category with id {id} does not exist");
            return BadRequest(ModelState.ToRequestError());
        }

        if (await messageBus.Send(new TransactionCategoriesContainTransactionsRequest
            {
                CategoryIds = new[]
                {
                    id
                },
                ActorId = GetAuthorizedUserId()
            }))
        {
            ModelState.AddModelError(nameof(id),
                $"Category with id {id} contains transactions. Please, delete them first");
            return BadRequest(ModelState.ToRequestError());
        }

        await messageBus.Publish(new DeleteTransactionCategoryMessage
        {
            Id = id
        });

        return Ok();
    }
}