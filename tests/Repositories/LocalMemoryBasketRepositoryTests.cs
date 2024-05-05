using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thunderwings.UnitTests.Repositories
{
    public class LocalMemoryBasketRepositoryTests
    {

        [Fact]
        public async Task GetBasketByUserId_ExistingUserId_ReturnsBasket()
        {
            //arrange
            var userId = 1;
            var data = new List<Basket>
            {
                new Basket { Id = 1, UserId = userId, Items = [] },
                new Basket { Id = 2, UserId = 2, Items = [] },
            };

            var repo = new LocalMemoryBasketRepository(data);

            //act

            var result = await repo.GetBasketByUserId(userId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetBasketByUserId_NonExistingId_ReturnsNull()
        {
            //arrange
            var userId = 4;
            var data = new List<Basket>
            {
                new Basket { Id = 1, UserId = 1, Items = [] },
                new Basket { Id = 2, UserId = 2, Items = [] },
            };

            var repo = new LocalMemoryBasketRepository(data);

            //act

            var result = await repo.GetBasketByUserId(userId);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateBasket_ValidUserId_CreatesAndReturnsBasket()
        {
            //arrange
            var repo = new LocalMemoryBasketRepository();
            // act
            var basket = await repo.CreateBasket(1);

            //assert
            Assert.NotNull(basket);
            Assert.Equal(1, basket.UserId);
            Assert.Equal(1, basket.Id); 
        }

        [Fact]
        public async Task AddItemToBasket_ExistingBasket_AddsItem()
        {
            // arrange
            var userId = 1;
            var basketId = 1;
            var data = new List<Basket>
            {
                new Basket { Id = basketId, UserId = userId, Items = [] },
            };

            var repo = new LocalMemoryBasketRepository(data);
            var item = new BasketItem { Id = 1, MilitaryJetId = 1 };

            // act
            var updatedBasket = await repo.AddItemToBasket(userId, item);

            // assert
            Assert.Single(updatedBasket.Items);
            Assert.Equal(basketId, updatedBasket.Id);
            Assert.Equal(item.Id, updatedBasket.Items.First().Id);
        }

        [Fact]
        public async Task AddItemToBasket_NonExistingBasket_CreatesBasketAndAddsItem()
        {
            //arrange
            var userId = 1;
            var repo = new LocalMemoryBasketRepository();
            var item = new BasketItem { Id = 1, MilitaryJetId = 1 };

            //act
            var updatedBasket = await repo.AddItemToBasket(userId, item);

            //assert
            Assert.NotNull(updatedBasket);
            Assert.Single(updatedBasket.Items);
            Assert.Equal(item, updatedBasket.Items[0]);
            Assert.Equal(userId, updatedBasket.UserId);
            Assert.Equal(1, updatedBasket.Id);
        }
        [Fact]
        public async Task RemoveItemFromBasket_NonExistingItem_ThrowsException()
        {
            // arrange
            var userId = 1;
            var itemId = 1;
            var data = new List<Basket>
            {
                new Basket { Id = 1, UserId = userId, Items = [] }
            };
            var repo = new LocalMemoryBasketRepository(data);

            //act
            await Assert.ThrowsAsync<BasketItemNotFoundException>(() => repo.RemoveItemFromBasket(userId, itemId));
        }

        [Fact]
        public async Task RemoveItemFromBasket_ExistingItem_RemovesItem()
        {
            //arrange
            var userId = 1;
            var itemId = 1;
            var data = new List<Basket>
            {
                new Basket { Id = 1, UserId = userId, Items = 
                    new List<BasketItem>
                    { 
                       new BasketItem() { Id = itemId, MilitaryJetId =1 }
                    } 
                },
            };
            var repo = new LocalMemoryBasketRepository(data);

            //act
            var result = await repo.RemoveItemFromBasket(userId, itemId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Empty(result.Items);
        }
    }
}
