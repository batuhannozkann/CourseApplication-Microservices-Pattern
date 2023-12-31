﻿using Course.SharedLibrary.ControllerBases;
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
        public async Task<IActionResult> AddOrder([FromBody]AddOrderCommand addOrderCommandd)
        {
            return CreateActionResultInstance(await _mediator.Send(addOrderCommandd));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderById()
        {
            
            return CreateActionResultInstance(await _mediator.Send(new GetOrdersByUserIdQuery
            {
                UserId = _sharedIdentityService.GetUserId

            }));
        }
    }
}
