using Application.Common;
using Application.Features.Basket.Queries;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket.Commands
{
    public record CreateBasketCommand(int UserId) : IRequest<Response<BasketDto>>;
    internal sealed class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Response<BasketDto>>
    {
        private readonly ILocalMemoryBasketRepository _localMemoryBasketRepository;
        private readonly IMapper _mapper;
        public CreateBasketCommandHandler(ILocalMemoryBasketRepository localMemoryBasketRepository, IMapper mapper)
        {
            _localMemoryBasketRepository = localMemoryBasketRepository;
            _mapper = mapper;
        }
        public async Task<Response<BasketDto>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket = await _localMemoryBasketRepository.GetBasketByUserId(request.UserId);
            if(existingBasket != null) 
            {
                var existing = _mapper.Map<BasketDto>(existingBasket);
                return Response<BasketDto>.Success(existing);
            }

            var createdBasket = await _localMemoryBasketRepository.CreateBasket(request.UserId);
            var dto = _mapper.Map<BasketDto>(createdBasket);
            return Response<BasketDto>.Success(dto);
        }
    }
}
