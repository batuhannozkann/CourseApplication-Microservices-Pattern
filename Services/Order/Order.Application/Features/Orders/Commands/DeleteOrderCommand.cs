using Course.SharedLibrary.Dtos;
using MediatR;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Commands
{
    public class DeleteOrderCommand:IRequest<ResponseDto<OrderDto>>
    {
        public int OrderId { get; set; }
    }
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ResponseDto<OrderDto>>
    {
        private IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseDto<OrderDto>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            Domain.OrderAggregate.Order order = _orderRepository.GetOrderById(request.OrderId);
            if (order == null) return ResponseDto<OrderDto>.Fail($"Order with id:{request.OrderId} has not found ", 400);
            _orderRepository.DeleteAsync(order);
            OrderDto deletedOrderDto = ObjectMapper.Mapper.Map<OrderDto>(order);
            _orderRepository.SaveChanges();
            return ResponseDto<OrderDto>.Success(deletedOrderDto, 200);
        }
    }
}
