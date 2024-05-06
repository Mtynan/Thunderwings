using Application.Features.Basket;
using Application.Features.Basket.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderwings.UnitTests.Handlers
{
    public class GetBasketQueryHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBasketQueryHandler _handler;

        public GetBasketQueryHandlerTests()
        {
            _mockRepo = new Mock<ILocalMemoryBasketRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBasketQueryHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_BasketExists_ReturnsBasketDto()
        {
            // arrange
            int userId = 1;
            var basket = new Basket(); 
            var basketDto = new BasketDto();
            var command = new GetBasketQuery(userId);

            _mockRepo.Setup(x => x.GetBasketByUserId(userId))
                           .ReturnsAsync(basket);
            _mockMapper.Setup(x => x.Map<BasketDto>(basket))
                       .Returns(basketDto);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Equal(basketDto, result.Data);
            _mockMapper.Verify(x => x.Map<BasketDto>(basket), Times.Once);
        }
        [Fact]
        public async Task Handle_BasketDoesNotExist_ThrowsBasketNotFoundException()
        {
            // arrange
            int userId = 1;
            var command = new GetBasketQuery(userId);
            _mockRepo.Setup(x => x.GetBasketByUserId(userId))
                           .ReturnsAsync((Basket?)null);  

            // act
            await Assert.ThrowsAsync<BasketNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
