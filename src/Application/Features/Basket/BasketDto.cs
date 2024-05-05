using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket
{
    public sealed record BasketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<BasketItemDto> Items { get; set; } = [];
    }
}
