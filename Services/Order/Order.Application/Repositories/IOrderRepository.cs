using Microsoft.EntityFrameworkCore;
using Order.Core.Assigments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Repositories
{
    public interface IOrderRepository:IBaseRepository<Domain.OrderAggregate.Order>
    {
        List<Domain.OrderAggregate.Order> GetOrdersByUserIdQuery(string userId);
        Domain.OrderAggregate.Order GetOrderById(int id);
    }
}
