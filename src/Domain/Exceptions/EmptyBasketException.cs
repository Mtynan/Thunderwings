using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EmptyBasketException : Exception
    {
        public EmptyBasketException(int basketId)
              : base($"The basket with the Id {basketId} is empty")
        {
        }

    }
}
