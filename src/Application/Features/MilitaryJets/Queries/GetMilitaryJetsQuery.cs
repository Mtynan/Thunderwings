using Application.Common;
using Domain.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.MilitaryJets.Queries
{
    public record GetMilitaryJetsQuery() : IRequest<Response<List<MilitaryJet>>>;

    internal sealed class GetMilitaryJetsQueryHandler : IRequestHandler<GetMilitaryJetsQuery, Response<List<MilitaryJet>>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;
        public GetMilitaryJetsQueryHandler(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }
        public async Task<Response<List<MilitaryJet>>> Handle(GetMilitaryJetsQuery request, CancellationToken cancellationToken)
        {
            var jets = await _localMemoryRepository.GetJets();
            if(jets is null)
            {
                return Response<List<MilitaryJet>>.Failure();
            }
            return Response<List<MilitaryJet>>.Success(jets);
        }
    }
}
