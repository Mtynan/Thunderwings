using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket
{
    public sealed record OrderConfirmationDto
    {
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
