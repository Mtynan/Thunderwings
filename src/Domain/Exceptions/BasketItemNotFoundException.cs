using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketItemNotFoundException : NotFoundException
    {
        public BasketItemNotFoundException(int basketId, int itemId)
           : base($"Basket Item not found for Basket with Basket id {basketId} and item id {itemId}")
        {
        }

    }
}
