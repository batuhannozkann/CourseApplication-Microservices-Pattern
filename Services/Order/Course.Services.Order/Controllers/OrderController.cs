using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Dtos;
using Course.SharedLibrary.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.Commands;
using Order.Application.Features.Orders.Queries;
using System.Collections.Generic;
using System.Net.Mime;

namespace Course.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;


        public OrderController(ISharedIdentityService sharedIdentityService,IMediator mediator)
        {
            _sharedIdentityService=sharedIdentityService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]AddOrderCommand addOrderCommand)
        {
            
            return CreateActionResultInstance(await _mediator.Send(addOrderCommand));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById()
        {
            
            return CreateActionResultInstance(await _mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = _sharedIdentityService.GetUserId

            }));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderByOrderId(int id)
        {

            return CreateActionResultInstance(await _mediator.Send(new GetOrderByOrderIdQuery { OrderId = id }));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand updateOrderCommand)
        {
            return CreateActionResultInstance(await _mediator.Send(updateOrderCommand));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderCommand deleteOrderCommand)
        {
            return CreateActionResultInstance(await _mediator.Send(deleteOrderCommand));
        }
    }
}
