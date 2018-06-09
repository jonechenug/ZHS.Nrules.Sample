using System;
using System.Linq;
using ZHS.Nrules.Core.Models.Orders;
using ZHS.Nrules.Infrastructure.Repository;

namespace ZHS.Nrules.Application.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order Get(String id)
        {
            return _orderRepository.Get(id);
        }

        public IQueryable<Order> QueryAble(){
            return _orderRepository.Queryable();
        }

        public Order InsertOrUpdate(Order o){
            return _orderRepository.InsertOrUpdate(o);
        }

    }
}