using Application.Common;
using Application.Features.MilitaryJets;
using Application.Features.MilitaryJets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Requests.MilitaryJets;

namespace Thunderwings.UnitTests.Controllers
{
    public class MilitaryJetsControllerTests
    {
        private readonly Mock<IMediator> _mockMediatR;
        private readonly MilitaryJetsController _militaryJetsController;

        public MilitaryJetsControllerTests()
        {
            _mockMediatR = new Mock<IMediator>();
            _militaryJetsController = new MilitaryJetsController(_mockMediatR.Object);
        }

        [Fact]  
        public async Task GetJets_ValidFilter_ReturnsExpectedResult()
        {
            //arrange
            var request = new GetMilitaryJetsRequest();
            var expectedResult = new List<MilitaryJetDto> { 
                new MilitaryJetDto { Id= 1, Country = "test", Manufacturer =  "Mt", Name= "mtjet", Price = 10, Role = "testrole", TopSpeed= 1, },
                new MilitaryJetDto { Id= 2, Country = "test2", Manufacturer =  "Mt2", Name= "mtje2t", Price = 12, Role = "testrole2", TopSpeed= 2, }
            };
            _mockMediatR.Setup(m => m.Send(It.IsAny<GetMilitaryJetsQuery>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Response<PagedResult<MilitaryJetDto>>
                 {
                     Data = new PagedResult<MilitaryJetDto>(expectedResult, expectedResult.Count, 1, 10),
                     IsSuccess = true
                 });

            //act
            var result = await _militaryJetsController.GetJets(request);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PagedResult<MilitaryJetDto>>(okResult.Value);
            
        }
        [Fact]
        public async Task GetJets_WhenNoResultsFound_ReturnsNoContent()
        {
            //arrange
            var request = new GetMilitaryJetsRequest();

            _mockMediatR.Setup(m => m.Send(It.IsAny<GetMilitaryJetsQuery>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Response<PagedResult<MilitaryJetDto>>
                 {
                     Data = null,
                     IsSuccess = true
                 });

            //act
            var result = await _militaryJetsController.GetJets(request);

            //assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
 
        }
    }
}
