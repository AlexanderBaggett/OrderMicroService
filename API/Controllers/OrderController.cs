using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderMicroservice.Db;
using OrderMicroservice.DomainModels;
using OrderMicroservice.RequestModels;
using OrderMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace OrderMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly OrderService service;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            _logger = logger;
            service = orderService;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById([FromRoute] long id)
        {
            var order = service.GetByID(id);
                    

            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult SearchOrders([FromQuery] long customerId, 
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime? endDate
            )
        {
            var orders = service.SearchOrders(customerId, startDate, endDate);
               
            return Ok(orders);
        }
        [HttpPost]
        public IActionResult CreateOrder()
        {

            return Ok(service.CreateOrder());

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateOrder([FromRoute]long id, [FromBody] OrderUpdateRequestModel request)
        {

            request.OrderId = id;
            var order = service.UpdateOrder(request);


            if (order == null)
            {
                return NotFound();
            }
            else if(order.Id == -1)
            {
                return BadRequest("Invalid Order Status");
            }

            return Ok(order);
        }
    }
}
