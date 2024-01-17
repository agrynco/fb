using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Currencies.GetAll;
using SlimMessageBus;

namespace Web.API.Controllers.Currencies;

public class CurrenciesController(IMessageBus messageBus) : AuthenticatedController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        GetAllCurrenciesResponse getAllCurrenciesResponse = await messageBus.Send(new GetAllCurrenciesRequest());

        return Ok(getAllCurrenciesResponse.Items);
    }
}