using Application.Features.Basket.Commands;
using Application.Features.Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests.Basket;

namespace WebApi.Controllers
{   /// <summary>
    /// Manages The Basket.
    /// </summary>
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Retrieves the users basket by User Id.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">When the basket doesn't exist.</response>
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetBasket(int userId)
        {
            var result = await _mediator.Send(new GetBasketQuery(userId));
            return HandleResult(result);
        }
        /// <summary>
        /// Creates the users basket.
        /// </summary>
        /// <param name="req">The create basket request.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">Validation error.</response>
        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketRequest req)
        {
            var result = await _mediator.Send(new CreateBasketCommand(req.UserId));
            return CreatedAtAction(nameof(GetBasket), new { userId = result.Data?.UserId }, result.Data);
        }
        /// <summary>
        /// Add an item to the basket. Creates a Basket for the user if one doesn't exist.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="req">The Military Jet you're adding the to basket.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">When Military Jet you're trying to add doesn't exist.</response>
        [HttpPost("{userId:int}/items")]
        public async Task<IActionResult> AddItemToBasket(int userId, AddBasketItemRequest req)
        {
            var result = await _mediator.Send(new AddBasketItemCommand(userId, req.MilitaryJetId));
            return HandleResult(result);
        }
        /// <summary>
        /// Removes an item from the basket. 
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="itemId">The Id of the Military Jet you wish to remove.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">When the specified item is not in the users basket or the user has no basket.</response>
        [HttpDelete("{userId:int}/items/{itemId:int}")]
        public async Task<IActionResult> RemoveItemFromBasket(int userId, int itemId)
        {
            var result = await _mediator.Send(new RemoveItemFromBasketCommand(userId, itemId));
            return HandleResult(result);
        }
        /// <summary>
        /// Checks out the basket and clears the users basket on success.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <response code="200">Successful operation.</response>
        /// <response code="209">When there are no items in the basket to checkout.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="404">When the user has no basket.</response>
        [HttpPost("checkout/{userId:int}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var result = await _mediator.Send(new CheckoutCommand(userId));
            return HandleResult(result);
        }
    }
}
