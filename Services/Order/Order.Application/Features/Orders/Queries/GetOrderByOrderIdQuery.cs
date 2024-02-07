using AutoMapper;
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

namespace Order.Application.Features.Orders.Queries
{
    public class GetOrderByOrderIdQuery:IRequest<ResponseDto<OrderDto>>
    {
        public int OrderId { get; set; }
    }
    public class GetOrderByOrderIdQueryHandler : IRequestHandler<GetOrderByOrderIdQuery, ResponseDto<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByOrderIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseDto<OrderDto>> Handle(GetOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {
            Domain.OrderAggregate.Order Order = _orderRepository.GetOrderById(request.OrderId);
            if (Order == null) return ResponseDto<OrderDto>.Fail($"Order with {request.OrderId} Id is not found", 400);
            OrderDto orderDto = ObjectMapper.Mapper.Map<OrderDto>(Order);
            orderDto.TotalPrice = Order.GetTotalPrice;
            return ResponseDto<OrderDto>.Success(orderDto, 200);
        }
    }
}
