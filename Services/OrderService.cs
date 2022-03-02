using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.Db;
using OrderMicroservice.DomainModels;
using OrderMicroservice.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderMicroservice.Services
{
    public class OrderService
    {
        private readonly OrderContext ctx;
        public OrderService(OrderContext context)
        {
            this.ctx = context;
        }

        public Order GetByID(long id)
        {
            return  ctx.Orders.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Order> SearchOrders(long customerId, DateTime startDate, DateTime? endDate)
        {
            return ctx.Orders.Where(x => x.CustomerId == customerId
                  && (x.CreatedDate >= startDate)
                  && (endDate.HasValue ? x.CreatedDate <= endDate : true));
                             
        }

        public Order CreateOrder()
        {

            var newOrder = new Order()
            {
                CreatedDate = DateTime.Now,
                OrderNumber = null,
                Total = null,
                OrderStatus = OrderStatus.Pending
            };
            ctx.Orders.Add(newOrder);
            ctx.SaveChanges();

            return newOrder; 
        }

        public Order UpdateOrder(OrderUpdateRequestModel request)
        {
            var order = this.GetByID(request.OrderId);
            if(order == null)
            {
                return null;
            }

            if (request.OrderStatus != null)
            {
                if (OrderStatus.IsValid(request.OrderStatus))
                {
                    order.OrderStatus = request.OrderStatus;
                }
                else
                {
                    return new Order() { Id = -1 };
                }
            }
            if (request.Total.HasValue)
            {
                order.Total = request.Total.Value;
            }
            ctx.SaveChanges();
            return order;
        }
    }
}
