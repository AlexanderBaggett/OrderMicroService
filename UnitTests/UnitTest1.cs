using Xunit;
using System;
using OrderMicroservice.Db;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Services;
using OrderMicroservice.DomainModels;
using OrderMicroservice.RequestModels;
using System.Linq;

namespace Tests
{
    public class UnitTests
    {


        [Fact]
        public void Invalid_Status_Returns_Minus_1()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);


            var request = new OrderUpdateRequestModel()
            {
                OrderId = 1,
                OrderStatus = "Invalid",
                Total = 50
            };
            var order = orderService.UpdateOrder(request);


            Assert.NotNull(order);
            Assert.Equal(order.Id, -1);

        }
        [Fact]
        public void Invalid_Status_Total_Not_Updated()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);


            var request = new OrderUpdateRequestModel()
            {
                OrderId = 1,
                OrderStatus = "Invalid",
                Total = 50
            };
            var order = orderService.UpdateOrder(request);

            var original = context.Orders.Single(x => x.Id == request.OrderId);


            Assert.NotEqual(order.Total, original.Total);
        }


        [Fact]
        public void Get_Order_By_Id_Returns_Correct_Order()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var order = orderService.GetByID(1);


            Assert.NotNull(order);
            Assert.Equal(100.55M, order.Total);

        }

        [Fact]
        public void New_Order_Has_Null_Total()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var order = orderService.CreateOrder();


            Assert.NotNull(order);
            Assert.Null(order.Total);

        }


        [Fact]
        public void New_Order_Has_Null_Order_Number()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var order = orderService.CreateOrder();


            Assert.NotNull(order);
            Assert.Null(order.OrderNumber);

        }

        [Fact]
        public void New_Order_Has_Pending_Status()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var order = orderService.CreateOrder();


            Assert.NotNull(order);
            Assert.Equal(order.OrderStatus, OrderStatus.Pending);

        }


        [Fact]
        public void Search_Today_Returns_One_Order()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var now = DateTime.Now;
            var orders = orderService.SearchOrders(1, now, null);

            Assert.Single(orders);
        }

        [Fact]
        public void Search_CustomerId_2_Returns_O_Orders()
        {
            var context = GetTestDbContext();
            var orderService = GetService(context);
            LoadTestData(context);

            var orders = orderService.SearchOrders(2, DateTime.Now, null);


            Assert.Empty(orders);
        }


        public OrderService GetService(OrderContext context)
        {
            var service = new OrderService(context);
            return service;
        }


        private OrderContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<OrderContext>();
            options.UseInMemoryDatabase("TestDB");
            var orderContext = new OrderContext(options.Options);

            return orderContext;
        }

        private void LoadTestData(OrderContext context)
        {
            var order1 = new Order()
            {
                CreatedDate = DateTime.Now.AddDays(1),
                CustomerId = 1,
                Id = 1,
                OrderNumber = "#1",
                OrderStatus = OrderStatus.Pending,
                Total = 100.55M
            };


            var order2 = new Order()
            {
                CreatedDate = DateTime.Now.AddDays(-2),
                CustomerId = 1,
                Id = 2,
                OrderNumber = "#2",
                OrderStatus = OrderStatus.Shipped,
                Total = 200M
            };

            var order3 = new Order()
            {
                CreatedDate = DateTime.Now.AddDays(-5),
                CustomerId = 1,
                Id = 3,
                OrderNumber = "#3",
                OrderStatus = OrderStatus.Delivered,
                Total = 500M
            };

            context.Orders.RemoveRange(context.Orders);
            context.SaveChanges();
            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);


            context.SaveChanges();

        }
    }
}