using Application.Common;
using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;

namespace Application.Features.MilitaryJets.Queries
{
    public record GetMilitaryJetsQuery(string? Name, string? Manufacturer, string? Country, string? Role, int? TopSpeed, int? Price) : IRequest<Response<List<MilitaryJetDto>>>;

    internal sealed class GetMilitaryJetsQueryHandler : IRequestHandler<GetMilitaryJetsQuery, Response<List<MilitaryJetDto>>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;
        private readonly IMapper _mapper;
        public GetMilitaryJetsQueryHandler(ILocalMemoryRepository localMemoryRepository, IMapper mapper)
        {
            _localMemoryRepository = localMemoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<MilitaryJetDto>>> Handle(GetMilitaryJetsQuery request, CancellationToken cancellationToken)
        {
            var jets = await _localMemoryRepository.GetJets(request.Name, request.Manufacturer, request.Country, request.Role, request.TopSpeed, request.Price);
            if(jets is null)
            {
                return Response<List<MilitaryJetDto>>.Failure();
            }
            var dtos = _mapper.Map<List<MilitaryJetDto>>(jets);
            return Response<List<MilitaryJetDto>>.Success(dtos);
        }
    }
}
