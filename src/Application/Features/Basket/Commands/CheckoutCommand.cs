using Application.Common;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket.Commands
{
    public record CheckoutCommand(int UserId) : IRequest<Response<OrderConfirmationDto>>;
    internal sealed class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, Response<OrderConfirmationDto>>
    {
        private readonly ILocalMemoryBasketRepository _localMemoryBasketRepository;
        private readonly ILocalMemoryJetRepository _localJetRepository;
        public CheckoutCommandHandler(ILocalMemoryBasketRepository localMemoryBasketRepository, ILocalMemoryJetRepository localJetRepository)
        {
            _localMemoryBasketRepository = localMemoryBasketRepository;
            _localJetRepository = localJetRepository;
        }
        public async Task<Response<OrderConfirmationDto>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            // I would wrap this in a transaction if this was talking to a real database
            var basket = await _localMemoryBasketRepository.GetBasketByUserId(request.UserId);
            if (basket == null)
            {
                throw new BasketNotFoundException(request.UserId);
            }
            if(basket.Items.Count == 0)
            {
                throw new EmptyBasketException(basket.Id);
            }
            var jetIds = basket.Items.Select(x => x.MilitaryJetId).ToList();
            var total = await _localJetRepository.CalculateTotalPrice(jetIds);
            var orderConfirmation = new OrderConfirmationDto
            {
                UserId = request.UserId,
                TotalPrice = total,
                PurchaseDate = DateTime.UtcNow
            };
            await _localMemoryBasketRepository.ClearBasket(request.UserId);
            return Response<OrderConfirmationDto>.Success(orderConfirmation);
        }
    }
}
