using Application.Common;
using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Domain.Common;

namespace Application.Features.MilitaryJets.Queries
{
    public record GetMilitaryJetsQuery(string? Name, string? Manufacturer, string? Country, string? Role, int? TopSpeed, int? Price, int PageNumber, int PageSize) : IRequest<Response<PagedResult<MilitaryJetDto>>>;

    internal sealed class GetMilitaryJetsQueryHandler : IRequestHandler<GetMilitaryJetsQuery, Response<PagedResult<MilitaryJetDto>>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;
        private readonly IMapper _mapper;
        public GetMilitaryJetsQueryHandler(ILocalMemoryRepository localMemoryRepository, IMapper mapper)
        {
            _localMemoryRepository = localMemoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<PagedResult<MilitaryJetDto>>> Handle(GetMilitaryJetsQuery request, CancellationToken cancellationToken)
        {
            var filter = new MilitaryJetFilter(request.Name, request.Manufacturer, request.Country, request.Role, request.TopSpeed, request.Price);
          
            var (filteredJets, totalCount) = await _localMemoryRepository.GetJets(filter, request.PageNumber, request.PageSize);

            if (filteredJets is null || filteredJets.Count == 0)
            {
                return Response<PagedResult<MilitaryJetDto>>.Failure();
            }

            var dtos = _mapper.Map<List<MilitaryJetDto>>(filteredJets);
            var pagedResult = new PagedResult<MilitaryJetDto>(dtos, totalCount, request.PageNumber, request.PageSize);
            return Response<PagedResult<MilitaryJetDto>>.Success(pagedResult);
        }
    }
}
