using Application.Features.Basket;
using Application.Features.Basket.Commands;
using AutoMapper;
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
    public class CreateBasketCommandHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBasketCommandHandler _handler;

        public CreateBasketCommandHandlerTests()
        {
            _mockRepo = new Mock<ILocalMemoryBasketRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateBasketCommandHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ExistingBasket_ReturnsMappedBasketDto()
        {
            // arrange
            int userId = 1;
            var existingBasket = new Basket();
            var basketDto = new BasketDto();
            var command = new CreateBasketCommand(userId);

            _mockRepo.Setup(x => x.GetBasketByUserId(userId))
                           .ReturnsAsync(existingBasket);
            _mockMapper.Setup(x => x.Map<BasketDto>(existingBasket))
                       .Returns(basketDto);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(basketDto, result.Data);
            _mockMapper.Verify(x => x.Map<BasketDto>(existingBasket), Times.Once);
            _mockRepo.Verify(x => x.CreateBasket(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NoExistingBasket_CreatesNewBasketAndReturnsMappedDto()
        {
            // arrange
            int userId = 2;
            var newBasket = new Basket();
            var basketDto = new BasketDto();
            var command = new CreateBasketCommand(userId);

            _mockRepo.Setup(x => x.GetBasketByUserId(userId))
                           .ReturnsAsync((Basket?)null);
            _mockRepo.Setup(x => x.CreateBasket(userId))
                           .ReturnsAsync(newBasket);
            _mockMapper.Setup(x => x.Map<BasketDto>(newBasket))
                       .Returns(basketDto);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(basketDto, result.Data);
            _mockMapper.Verify(x => x.Map<BasketDto>(newBasket), Times.Once);
            _mockRepo.Verify(x => x.CreateBasket(userId), Times.Once);
        }
    }
}
