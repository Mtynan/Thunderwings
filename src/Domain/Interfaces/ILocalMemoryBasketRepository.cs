using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILocalMemoryBasketRepository
    {
        Task<Basket?> GetBasketByUserId(int userId);
        Task<Basket> CreateBasket(int userId);
        Task<Basket> AddItemToBasket(int userId, BasketItem item);
        Task<Basket?> RemoveItemFromBasket(int userId, int itemId);
        Task ClearBasket(int userId);


    }
}
