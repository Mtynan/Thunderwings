﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket : EntityBase
    {
        public int UserId { get; set; }
        public List<BasketItem> Items { get; set; } = [];
    }
}
