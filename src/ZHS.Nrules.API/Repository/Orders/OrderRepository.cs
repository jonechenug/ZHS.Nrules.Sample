using System;
using System.Collections.Generic;
using ZHS.Nrules.API.Repository;
using ZHS.Nrules.Core.Models.Orders;

namespace ZHS.Nrules.Infrastructure.Repository.Orders
{
    public class OrderRepository : BaseFakeRepository<Order, String>, IOrderRepository
    {
        public OrderRepository()
        {

            this.Add(new Order
            {
                Id = "1",
                Address = "测试地址",
                BuyerId = "1",
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Submitted,
                OrderItems = new List<OrderItem>
              {
                  new OrderItem
                  {
                      ProductId = "1",
                      OrderId = "1",
                      ProductName = "测试商品",
                      Promotions = new List<OrderItemPromotion>(),
                      Quantity = 1,
                      UnitPrice = 100
                  }
              }
            });

            this.Add(new Order
            {
                Id = "2",
                Address = "测试地址",
                BuyerId = "1",
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Submitted,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = "1",
                        OrderId = "2",
                        ProductName = "测试商品",
                        Promotions = new List<OrderItemPromotion>(),
                        Quantity = 2,
                        UnitPrice = 100
                    }
                }
            });

        }
    }
}