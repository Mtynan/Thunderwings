using Application.Features.Basket.Commands;
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
    public class RemoveItemFromBasketCommandHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _mockRepository;
        private readonly RemoveItemFromBasketCommandHandler _handler;

        public RemoveItemFromBasketCommandHandlerTests()
        {
            _mockRepository = new Mock<ILocalMemoryBasketRepository>();
            _handler = new RemoveItemFromBasketCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ItemRemoved_ReturnsSuccessNoContent()
        {
            // arrange
            int userId = 1, itemId = 2;
            var command = new RemoveItemFromBasketCommand(userId, itemId);
            var basket = new Basket(); 
            _mockRepository.Setup(x => x.RemoveItemFromBasket(userId, itemId))
                           .ReturnsAsync(basket);

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Data);  
            _mockRepository.Verify(x => x.RemoveItemFromBasket(userId, itemId), Times.Once);
        }
        [Fact]
        public async Task Handle_BasketNotFound_ThrowsBasketNotFoundException()
        {
            //arrange
            int userId = 1, itemId = 2;
            var command = new RemoveItemFromBasketCommand(userId, itemId);
            _mockRepository.Setup(x => x.RemoveItemFromBasket(userId, itemId))
                           .ReturnsAsync((Basket?)null);  

            // act
            await Assert.ThrowsAsync<BasketNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            _mockRepository.Verify(x => x.RemoveItemFromBasket(userId, itemId), Times.Once);
        }

    }
}
