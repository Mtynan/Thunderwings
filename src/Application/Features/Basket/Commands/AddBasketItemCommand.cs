using Application.Common;
using AutoMapper;
using Domain.Entities;
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
    public record AddBasketItemCommand(int UserId, int MilitaryJetId) : IRequest<Response<BasketDto>>;
    internal sealed class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommand, Response<BasketDto>>
    {
        private readonly ILocalMemoryBasketRepository _localMemoryBasketRepository;
        private readonly ILocalMemoryJetRepository _localJetRepository;
        private readonly IMapper _mapper;
        public AddBasketItemCommandHandler(ILocalMemoryBasketRepository localMemoryBasketRepository, IMapper mapper, ILocalMemoryJetRepository localJetRepository)
        {
            _localMemoryBasketRepository = localMemoryBasketRepository;
            _mapper = mapper;
            _localJetRepository = localJetRepository;
        }
        public async Task<Response<BasketDto>> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var jetExists = _localJetRepository.Exists(request.MilitaryJetId);
            if (!jetExists)
            {
                throw new MilitaryJetNotFoundException(request.MilitaryJetId);
            }
            var newItem = new BasketItem() { MilitaryJetId = request.MilitaryJetId };
            var updatedBasket = await _localMemoryBasketRepository.AddItemToBasket(request.UserId, newItem);
            var dto = _mapper.Map<BasketDto>(updatedBasket);
            return Response<BasketDto>.Success(dto);

        }
    }
}

