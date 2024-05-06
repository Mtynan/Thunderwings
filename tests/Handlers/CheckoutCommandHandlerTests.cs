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
    public class CheckoutCommandHandlerTests
    {
        private readonly Mock<ILocalMemoryBasketRepository> _basketMockRepo;
        private readonly Mock<ILocalMemoryJetRepository> _jetMockRepo;
        private readonly CheckoutCommandHandler _handler;

        public CheckoutCommandHandlerTests()
        {
            _basketMockRepo = new Mock<ILocalMemoryBasketRepository>();
            _jetMockRepo = new Mock<ILocalMemoryJetRepository>();
            _handler = new CheckoutCommandHandler(_basketMockRepo.Object, _jetMockRepo.Object);
        }

        [Fact]
        public async Task Handle_BasketNotFound_ThrowsBasketNotFoundException()
        {
            //arrange
            var userId = 1;
            var command = new CheckoutCommand(userId);
            _basketMockRepo.Setup(x => x.GetBasketByUserId(userId))
                .ReturnsAsync((Basket?)null);

            // act
            await Assert.ThrowsAsync<BasketNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_EmptyBasket_ThrowsEmptyBasketException()
        {
            //arrange
            var userId = 1;
            var basket = new Basket { Id = 1, UserId = userId, Items = [] };
            var command = new CheckoutCommand(userId);
            _basketMockRepo.Setup(x => x.GetBasketByUserId(It.IsAny<int>()))
                .ReturnsAsync(basket);

            // act
            await Assert.ThrowsAsync<EmptyBasketException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidBasket_ReturnsSuccessfulOrderConfirmation()
        {
            //arrange
            var userId = 1;
            var command = new CheckoutCommand(userId);
            var basket = new Basket
            {
                Id = 1,
                UserId = userId,
                Items = new List<BasketItem> { new BasketItem { MilitaryJetId = 1 } }
            };
            _basketMockRepo.Setup(x => x.GetBasketByUserId(It.IsAny<int>()))
                .ReturnsAsync(basket);

            _jetMockRepo.Setup(x => x.CalculateTotalPrice(It.IsAny<List<int>>()))
                .ReturnsAsync(100000); 

            //act
            var response = await _handler.Handle(command, CancellationToken.None);

            //assert
            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Data);
            Assert.Equal(100000, response.Data.TotalPrice);
            Assert.Equal(userId, response.Data.UserId);
            _basketMockRepo.Verify(x => x.ClearBasket(userId), Times.Once);
        }
    }
}
