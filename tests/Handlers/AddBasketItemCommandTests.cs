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
    public class AddBasketItemCommandHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddBasketItemCommandHandler _handler;

        public AddBasketItemCommandHandlerTests()
        {
            _mockRepository = new Mock<ILocalMemoryBasketRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new AddBasketItemCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_AddsItemAndReturnsSuccess()
        {
            // arrange
            var command = new AddBasketItemCommand(1, 1);
            var basket = new Basket();
            var basketDto = new BasketDto();

            _mockRepository.Setup(x => x.AddItemToBasket(command.UserId, It.IsAny<BasketItem>()))
                           .ReturnsAsync(basket);

            _mockMapper.Setup(x => x.Map<BasketDto>(basket))
                       .Returns(basketDto);

            // act
            var response = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(response.IsSuccess);
            Assert.Equal(basketDto, response.Data);
            _mockRepository.Verify(x => x.AddItemToBasket(command.UserId, It.IsAny<BasketItem>()), Times.Once);
            _mockMapper.Verify(x => x.Map<BasketDto>(basket), Times.Once);
        }
    }
}
