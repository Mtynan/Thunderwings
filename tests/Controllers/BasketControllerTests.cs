using Application.Common;
using Application.Features.Basket;
using Application.Features.Basket.Commands;
using Application.Features.Basket.Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Requests.Basket;

namespace Thunderwings.UnitTests.Controllers
{
    public class BasketControllerTests
    {
        private readonly Mock<IMediator> _mockMediatR;
        private readonly BasketController _basketController;

        public BasketControllerTests()
        {
            _mockMediatR = new Mock<IMediator>();
            _basketController = new BasketController(_mockMediatR.Object);
        }

        [Fact]
        public async Task GetBasket_ValidUser_ReturnsExpectedResult()
        {
            //arrange
            int userId = 1;
            var basket = new BasketDto { Id = 1, UserId = userId, Items = [] };
            var basketResponse = new Response<BasketDto>
            {
                IsSuccess = true,
                Data = basket,
            };

            _mockMediatR.Setup(m => m.Send(It.IsAny<GetBasketQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(basketResponse));

            //act
            var result = await _basketController.GetBasket(userId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(basketResponse.Data, okResult.Value);
        }

        [Fact]
        public async Task CreateBasket_ValidUser_ReturnsCreatedAt()
        {
            //arrange
            int userId = 1;
            var request = new CreateBasketRequest { UserId = userId };
            var basket = new BasketDto { Id = 1, UserId = userId, Items = [] };
            var createBasketResponse = new Response<BasketDto>
            {
                IsSuccess = true,
                Data = basket,
            };

            _mockMediatR.Setup(m => m.Send(It.IsAny<CreateBasketCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(createBasketResponse));

            //act
            var result = await _basketController.CreateBasket(request);

            //assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.NotNull(createdResult.Value);
            Assert.Equal("GetBasket", createdResult.ActionName);
            Assert.Equal(createBasketResponse.Data, createdResult.Value);
            Assert.Equal(basket, createdResult.Value);
        }

        [Fact]
        public async Task AddItemToBasket_ValidRequest_ReturnsExpectedResult()
        {
            //arrange
            int userId = 1;
            var request = new AddBasketItemRequest { MilitaryJetId = 5 };
            var addItemResponse = new Response<BasketDto>
            {
                IsSuccess = true,
                Data = new BasketDto { Id = 1, UserId = userId, Items = new List<BasketItemDto> { new BasketItemDto { Id = 1, MilitaryJetId = 1 } } }
            };

            _mockMediatR.Setup(m => m.Send(It.IsAny<AddBasketItemCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(addItemResponse);

            //act
            var result = await _basketController.AddItemToBasket(userId, request);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(addItemResponse.Data, okResult.Value);
        }

        [Fact]
        public async Task RemoveItemFromBasket_ItemExists_ReturnsExpectedResult()
        {
            // arrange
            int userId = 1, itemId = 1;
            var deleteItemResponse = Response<BasketDto>.SuccessNoContent();

            _mockMediatR.Setup(m => m.Send(It.IsAny<RemoveItemFromBasketCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(deleteItemResponse);

            // Act
            var result = await _basketController.RemoveItemFromBasket(userId, itemId);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, okResult.StatusCode);
        }

        [Fact]
        public async Task Checkout_ValidData_ReturnsExpectedResult()
        {
            //arrange
            int userId = 1;
            var checkoutResponse = new Response<OrderConfirmationDto>
            {
                IsSuccess = true,
                Data = new OrderConfirmationDto { UserId = userId, PurchaseDate = DateTime.Now, TotalPrice = 10 }
            };

            _mockMediatR.Setup(m => m.Send(It.IsAny<CheckoutCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(checkoutResponse);

            // Act
            var result = await _basketController.Checkout(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
