using Course.SharedLibrary.Dtos;
using MediatR;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Application.Repositories;
using Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands
{
    public class UpdateOrderCommand:IRequest<ResponseDto<UpdatedOrderDto>>
    {
        public int OrderId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ResponseDto<UpdatedOrderDto>>
    {
        private IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseDto<UpdatedOrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Domain.OrderAggregate.Order order =  _orderRepository.GetOrderById(request.OrderId);
            foreach(var orderItem in request.OrderItems)
            {
                
                OrderItem _orderItem = order.OrderItems.Where(x => x.ProductId == orderItem.ProductId).FirstOrDefault();
       
                    decimal price = _orderItem.Price;
                    string productName = orderItem.ProductName ?? _orderItem.ProductName;
                    if (_orderItem.Price != orderItem.Price) price = orderItem.Price;
                    string pictureUrl = orderItem.PictureUrl ?? _orderItem.PictureUrl;
                    _orderItem.UpdateOrderItem(productName, pictureUrl, price);
                
            }
            order.SetAddress(ObjectMapper.Mapper.Map<Address>(request.Address));
            _orderRepository.SaveChanges();
            return ResponseDto<UpdatedOrderDto>.Success(data:ObjectMapper.Mapper.Map<UpdatedOrderDto>(order),200);
        }
    }
}
