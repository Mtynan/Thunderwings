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
    public record RemoveItemFromBasketCommand(int UserId, int ItemId) : IRequest<Response<BasketDto>>;
    internal sealed class RemoveItemFromBasketCommandHandler : IRequestHandler<RemoveItemFromBasketCommand, Response<BasketDto>>
    {
        private readonly ILocalMemoryBasketRepository _localMemoryRepository;
        public RemoveItemFromBasketCommandHandler(ILocalMemoryBasketRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }
        public async Task<Response<BasketDto>> Handle(RemoveItemFromBasketCommand request, CancellationToken cancellationToken)
        {
            var updatedBasket = await _localMemoryRepository.RemoveItemFromBasket(request.UserId, request.ItemId);
            if(updatedBasket is null)
            {
                throw new BasketNotFoundException(request.UserId);
            }
            return Response<BasketDto>.SuccessNoContent();
        }
    }
}
