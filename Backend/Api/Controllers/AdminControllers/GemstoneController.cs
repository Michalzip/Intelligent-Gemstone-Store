using IntelligentStore.Application.DTOs;
using IntelligentStore.Application.Functions.AdminFunctions.Commands;
using IntelligentStore.Application.Functions.AdminFunctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Providers;
using Swashbuckle.AspNetCore.Annotations;

namespace IntelligentStore.Api.Controllers.AdminControllers;

[Tags("Admin/GemstoneController")]
[ApiController]
[Authorize(Policy = "Admin")]
public class GemstoneController : ControllerBase
{
    private readonly IMediator _mediator;

    public GemstoneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Admin get list of stones")]
    [HttpGet("admin/get-list-of-stone")]
    public async Task<List<GemstoneModelProvider>> GetAllGemstoneData([FromQuery] PriceDto priceDto)
    {
        return await _mediator.Send(
            new GetListOfStonesQuery(
                priceDto.Phrase,
                priceDto.MinPrice,
                priceDto.MaxPrice,
                priceDto.PageNumber
            )
        );
    }

    [SwaggerOperation(Summary = "Admin order some stones")]
    [HttpPost("admin/order-stones")]
    public async Task OrderStonesToShop()
    {
        await _mediator.Send(new OrderStonesToShopCommand());
    }
}
