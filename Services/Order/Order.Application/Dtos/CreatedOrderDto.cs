﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Dtos
{
    public class CreatedOrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddressDto Address { get; set; }

        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
        public decimal TotalPrice => OrderItems.Select(x => x.Price).Sum();
    }
}
