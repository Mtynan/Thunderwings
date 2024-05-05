using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket
{
    public sealed record BasketItemDto
    {
        public int Id { get; set; }
        public int MilitaryJetId { get; set; }
    }
}
