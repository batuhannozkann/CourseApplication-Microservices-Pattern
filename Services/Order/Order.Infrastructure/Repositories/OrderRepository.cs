﻿using Order.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Application.Repositories;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Domain.OrderAggregate.Order, OrderDbContext>, IOrderRepository
    {
        private OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public List<Domain.OrderAggregate.Order> GetOrdersByUserIdQuery(string userId)
        {
            List<Domain.OrderAggregate.Order> Orders = _dbContext.Set<Domain.OrderAggregate.Order>().Include(x=>x.OrderItems).Where(x => x.BuyerId == userId).ToList();
            return Orders;
        }
        public Domain.OrderAggregate.Order GetOrderById(int id)
        {
            Domain.OrderAggregate.Order Order = _dbContext.Orders.Include(x => x.OrderItems).Where(x => x.Id == id).FirstOrDefault();
            return Order;
        }

    }
}
