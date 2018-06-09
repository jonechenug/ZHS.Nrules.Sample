using System;
using ZHS.Nrules.Core.Models.Orders;

namespace ZHS.Nrules.Infrastructure.Repository
{
    public interface IOrderRepository:ICRUDRepository<Order,String >
    {
    }
}