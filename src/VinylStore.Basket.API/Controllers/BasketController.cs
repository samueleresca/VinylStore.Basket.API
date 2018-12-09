using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Basket.API.Infrastructure.Filters;
using VinylStore.Basket.Domain.Requests.Basket;

namespace VinylStore.Basket.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    [JsonException]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IActionResult>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBasketRequest {Id = id});
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBasketRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, null);
        }

        [HttpPut("{basketId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Put(Guid basketId, Guid id)
        {
            var result = await _mediator.Send(new UpdateBasketItemQuantityRequest
            {
                BasketId = basketId,
                BasketItemId = id,
                IsAddOperation = true
            });

            return Ok(result);
        }

        [HttpDelete("{basketId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Delete(Guid basketId, Guid id)
        {
            var result = await _mediator.Send(new UpdateBasketItemQuantityRequest
            {
                BasketId = basketId,
                BasketItemId = id,
                IsAddOperation = false
            });

            return Ok(result);
        }
    }
}