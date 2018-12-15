﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Cart.API.Infrastructure.Filters;
using VinylStore.Cart.Domain.Requests.Cart;

namespace VinylStore.Cart.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [JsonException]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCartRequest {Id = id});
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCartRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, null);
        }

        [HttpPut("{cartId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Put(Guid cartId, Guid id)
        {
            var result = await _mediator.Send(new UpdateCartItemQuantityRequest
            {
                CartId = cartId,
                CartItemId = id,
                IsAddOperation = true
            });

            return Ok(result);
        }

        [HttpDelete("{cartId:guid}/items/{id:guid}")]
        public async Task<IActionResult> Delete(Guid cartId, Guid id)
        {
            var result = await _mediator.Send(new UpdateCartItemQuantityRequest
            {
                CartId = cartId,
                CartItemId = id,
                IsAddOperation = false
            });

            return Ok(result);
        }
    }
}