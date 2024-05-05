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
        private readonly ILocalMemoryBasketRepository _localMemoryRepository;
        private readonly IMapper _mapper;
        public CreateBasketCommandHandler(ILocalMemoryBasketRepository localMemoryRepository, IMapper mapper)
        {
            _localMemoryRepository = localMemoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<BasketDto>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket = await _localMemoryRepository.GetBasketByUserId(request.UserId);
            if(existingBasket != null) 
            {
                var existing = _mapper.Map<BasketDto>(existingBasket);
                return Response<BasketDto>.Success(existing);
            }

            var createdBasket = await _localMemoryRepository.CreateBasket(request.UserId);
            var dto = _mapper.Map<BasketDto>(createdBasket);
            return Response<BasketDto>.Success(dto);
        }
    }
}
