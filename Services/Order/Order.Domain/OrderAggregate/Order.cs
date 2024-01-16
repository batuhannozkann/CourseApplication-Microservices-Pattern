using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Core.Assigments;

namespace Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get;private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

        public Order()
        {

        }
        public Order(string buyerId, Address address)
        {
            CreatedDate=DateTime.Now;
            Address = address;
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productId,string productName,decimal price,string pictureUrl)
        {
            var existProduct = _orderItems.Exists(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName,pictureUrl,price);
                _orderItems.Add(newOrderItem);
            }
        }
        public void SetAddress(Address address)
        {
            Address = address;
        }
        
    }
}
