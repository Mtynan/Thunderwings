using Application.Features.Basket.Commands;
using Application.Features.Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests.Basket;

namespace WebApi.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetBasket(int userId)
        {
            var result = await _mediator.Send(new GetBasketQuery(userId));
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketRequest req)
        {
            var result = await _mediator.Send(new CreateBasketCommand(req.UserId));
            return CreatedAtAction(nameof(GetBasket), new { userId = result.Data?.UserId }, result.Data);
        }

        [HttpPost("{userId:int}/items")]
        public async Task<IActionResult> AddItemToBasket(int userId, AddBasketItemRequest req)
        {
            var result = await _mediator.Send(new AddBasketItemCommand(userId, req.MilitaryJetId));
            return HandleResult(result);
        }

        [HttpDelete("{userId:int}/items/{itemId:int}")]
        public async Task<IActionResult> RemoveItemFromBasket(int userId, int itemId)
        {
            var result = await _mediator.Send(new RemoveItemFromBasketCommand(userId, itemId));
            return HandleResult(result);
        }

        [HttpPost("checkout/{userId:int}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var result = await _mediator.Send(new CheckoutCommand(userId));
            return HandleResult(result);
        }
    }
}
