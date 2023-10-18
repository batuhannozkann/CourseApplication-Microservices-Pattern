using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.SharedLibrary.Dtos;
using MediatR;
using Order.Application.Dtos;
using Order.Application.Repositories;
using Order.Domain.OrderAggregate;

namespace Order.Application.Features.Orders.Commands
{
    public class AddOrderCommand:IRequest<ResponseDto<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }

    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand,ResponseDto<CreatedOrderDto>>
    {
        private IOrderRepository _orderRepository;

        public AddOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<ResponseDto<CreatedOrderDto>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Address address = new Address(request.Address.Province, request.Address.District, request.Address.Street,
                request.Address.ZipCode, request.Address.Line);
            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(request.BuyerId,address);
            request.OrderItems.ForEach(
                x =>
                {
                    order.AddOrderItem(x.ProductId,x.ProductName,x.Price,x.PictureUrl);
                }
            );
            await _orderRepository.AddAsync(order);
            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto(){OrderId = order.Id},201);
        }
    }
}
