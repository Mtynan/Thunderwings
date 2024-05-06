using Application.Features.Basket;
using Application.Features.Basket.Commands;
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
    public class AddBasketItemCommandHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _mockRepo;
        private readonly Mock<ILocalMemoryJetRepository> _mockJetRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddBasketItemCommandHandler _handler;

        public AddBasketItemCommandHandlerTests()
        {
            _mockRepo = new Mock<ILocalMemoryBasketRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockJetRepo = new Mock<ILocalMemoryJetRepository>();
            _handler = new AddBasketItemCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockJetRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_AddsItemAndReturnsSuccess()
        {
            // arrange
            var command = new AddBasketItemCommand(1, 1);
            var basket = new Basket();
            var basketDto = new BasketDto();

            _mockRepo.Setup(x => x.AddItemToBasket(command.UserId, It.IsAny<BasketItem>()))
                           .ReturnsAsync(basket);

            _mockMapper.Setup(x => x.Map<BasketDto>(basket))
                       .Returns(basketDto);

            _mockJetRepo.Setup(x => x.Exists(command.MilitaryJetId))
                .Returns(true);
            // act
            var response = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(response.IsSuccess);
            Assert.Equal(basketDto, response.Data);
            _mockRepo.Verify(x => x.AddItemToBasket(command.UserId, It.IsAny<BasketItem>()), Times.Once);
            _mockMapper.Verify(x => x.Map<BasketDto>(basket), Times.Once);
        }

        [Fact]
        public async Task Handle_JetDoesNotExist_ThrowsMilitaryJetNotFoundException()
        {
            // arrange
            var command = new AddBasketItemCommand(1, 1);
            _mockJetRepo.Setup(x => x.Exists(command.MilitaryJetId))
                              .Returns(false);  

            //act
            await Assert.ThrowsAsync<MilitaryJetNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(x => x.AddItemToBasket(It.IsAny<int>(), It.IsAny<BasketItem>()), Times.Never);
        }
    }
}
