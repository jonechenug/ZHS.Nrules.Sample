using System;
using ZHS.Nrules.Core.Models.Orders;

namespace ZHS.Nrules.API.Models.Output
{
    public class AllPromotionForOrderOutput
    {
        public Order Order { get; set; }
        public Decimal TotalPrice => Order.GetTotalPrice();
        public Decimal TotalReduction => Order.GetTotalReduction();

        public AllPromotionForOrderOutput(Order order)
        {
            Order = order;
        }
    }
}