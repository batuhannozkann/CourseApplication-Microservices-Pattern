using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.SharedLibrary.Dtos;
using MediatR;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Application.Repositories;

namespace Order.Application.Features.Orders.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<ResponseDto<List<OrderDto>>>
    {
        public string UserId { get; set; }
    } 
    
    class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private IOrderRepository _orderRepository;

        public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var Orders =  _orderRepository.GetOrdersByUserIdQuery(request.UserId);
            if (!Orders.Any())
            {
                return ResponseDto<List<OrderDto>>.Fail("Any Order is not found",400);
            }
            List<OrderDto> orderDtos = ObjectMapper.Mapper.Map<List<Domain.OrderAggregate.Order>,List<OrderDto>>(Orders.ToList());
            return ResponseDto<List<OrderDto>>.Success(orderDtos,200);
        }
    }
}
