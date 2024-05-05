using Application.Features.MilitaryJets;
using Application.Features.MilitaryJets.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderwings.UnitTests.Handlers
{
    public class GetMilitaryJetsQueryHandlerTests
    {
        private readonly Mock<ILocalMemoryJetRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetMilitaryJetsQueryHandler _handler;

        public GetMilitaryJetsQueryHandlerTests()
        {
            _mockRepo = new Mock<ILocalMemoryJetRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetMilitaryJetsQueryHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccessfulResponse_WithData()
        {
            // arrange
            var jets = new List<MilitaryJet> { 
                new MilitaryJet { Id = 1, Country = "test", Manufacturer = "Mt", Name = "mtjet", Price = 10, Role = "testrole", TopSpeed = 1, },
                new MilitaryJet { Id= 2, Country = "test2", Manufacturer =  "Mt2", Name= "mtje2t", Price = 12, Role = "testrole2", TopSpeed= 2, }
            };
            var dtos =  new List<MilitaryJetDto> {
                new MilitaryJetDto { Id= 1, Country = "test", Manufacturer =  "Mt", Name= "mtjet", Price = 10, Role = "testrole", TopSpeed= 1, },
                new MilitaryJetDto { Id= 2, Country = "test2", Manufacturer =  "Mt2", Name= "mtje2t", Price = 12, Role = "testrole2", TopSpeed= 2, }
            };
            var filter = new MilitaryJetFilter();
            var query = new GetMilitaryJetsQuery(null, null, null, null, null, null, 1, 10);

            _mockRepo.Setup(repo => repo.GetJets(filter, 1, 10))
                .ReturnsAsync((jets, jets.Count));
            _mockMapper.Setup(mapper => mapper.Map<List<MilitaryJetDto>>(jets))
                .Returns(dtos);

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data?.Items);
            Assert.Equal(dtos.Count, result.Data.Items.Count);
            Assert.Equal(dtos, result.Data.Items);
        }

        [Fact]
        public async Task Handle_ReturnsSuccessNoContentWhenNoDataFound()
        {
            // arrange
            var filter = new MilitaryJetFilter();
            var query = new GetMilitaryJetsQuery(null, null, null, null, null, null, 1, 10);

            _mockRepo.Setup(repo => repo.GetJets(filter, 1, 10))
                .ReturnsAsync((new List<MilitaryJet>(), 0)); 

            // act
            var result = await _handler.Handle(query, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Data);
        }
    }
}
