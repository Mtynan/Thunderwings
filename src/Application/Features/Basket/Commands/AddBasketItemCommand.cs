using Application.Common;
using AutoMapper;
using Domain.Entities;
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
        private readonly ILocalMemoryBasketRepository _localMemoryRepository;
        private readonly IMapper _mapper;
        public AddBasketItemCommandHandler(ILocalMemoryBasketRepository localMemoryRepository, IMapper mapper)
        {
            _localMemoryRepository = localMemoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<BasketDto>> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = new BasketItem() { MilitaryJetId = request.MilitaryJetId };
            var updatedBasket = await _localMemoryRepository.AddItemToBasket(request.UserId, newItem);
            var dto = _mapper.Map<BasketDto>(updatedBasket);
            return Response<BasketDto>.Success(dto);

        }
    }
}

