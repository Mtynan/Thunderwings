using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LocalMemoryBasketRepository : ILocalMemoryBasketRepository
    {
        private List<Basket> _basketList;
        private int _nextBasketId = 1;
        private int _nextBasketItemId = 1;

        public LocalMemoryBasketRepository(List<Basket>? seedData = null)
        {
            if (seedData == null)
            {
                _basketList = [];
            }
            else
            {
                _basketList = seedData;
            }
        }

        public Task<Basket?> GetBasketByUserId(int userId)
        {
            var basket = _basketList.FirstOrDefault(x => x.UserId == userId);
            return Task.FromResult(basket);
        }

        public Task<Basket> CreateBasket(int userId)
        {
            var basket = new Basket { UserId = userId };
            basket.Id = _nextBasketId++;
            _basketList.Add(basket);
            return Task.FromResult(basket);
        }

        public Task<Basket> AddItemToBasket(int userId, BasketItem item)
        {
            var basket = _basketList.FirstOrDefault(x=>x.UserId == userId);
            if (basket == null)
            {
                basket = new Basket
                {
                    Id = _nextBasketId++,
                    UserId = userId,
                    Items = []
                };
                _basketList.Add(basket);
            }
            item.Id = _nextBasketItemId++;
            basket.Items.Add(item);
            return Task.FromResult(basket);
        }

        public Task<Basket?> RemoveItemFromBasket(int userId, int itemId)
        {
            var basket = _basketList.FirstOrDefault(x => x.UserId == userId);
            if (basket != null)
            {
                var item = basket.Items.FirstOrDefault(x => x.Id == itemId);
                if (item == null)
                {
                    throw new BasketItemNotFoundException(basket.Id, itemId);
                }
                basket.Items.Remove(item);
            }
            return Task.FromResult(basket);

        }
        public Task ClearBasket(int userId)
        {
            var basket = _basketList.FirstOrDefault(b => b.UserId == userId);
            if (basket != null)
            {
                basket.Items.Clear();  
            }
            return Task.CompletedTask;
        }

    }
}
