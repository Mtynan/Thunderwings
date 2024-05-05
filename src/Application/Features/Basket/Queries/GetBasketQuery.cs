using Application.Common;
using Application.Features.MilitaryJets;
using Application.Features.MilitaryJets.Queries;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket.Queries
{
    public record GetBasketQuery(int UserId) : IRequest<Response<BasketDto>>;

    internal sealed class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Response<BasketDto>>
    {
        private readonly ILocalMemoryBasketRepository _localMemoryRepository;
        private readonly IMapper _mapper;
        public GetBasketQueryHandler(ILocalMemoryBasketRepository localMemoryRepository, IMapper mapper)
        {
            _localMemoryRepository = localMemoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _localMemoryRepository.GetBasketByUserId(request.UserId);
            if(basket is null)
            {
                throw new BasketNotFoundException(request.UserId);
            }
            var dto = _mapper.Map<BasketDto>(basket);
            return Response<BasketDto>.Success(dto);
        }
    }
}
