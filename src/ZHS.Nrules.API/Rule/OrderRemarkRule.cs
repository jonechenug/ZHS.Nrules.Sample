using Microsoft.EntityFrameworkCore.Internal;
using NRules.Fluent.Dsl;
using ZHS.Nrules.Core.Models.Orders;

namespace ZHS.Nrules.API.Rule
{
    public class OrderRemarkRule: NRules.Fluent.Dsl.Rule
    {
        public override void Define()
        {
            Order order = null;

            //When().Match<Order>(() => order)
            //    .Query(()=>order,r=>r.Match<Order>(
            //                                                    o=>o.OrderItems.Exists(i=>i.Promotions!=null&&i.Promotions.Count>0)));

            When().Match<Order>(() => order);


            Then()
                .Do(ctx=> order.AddRemark("  OrderRemark "));
        }
    }
}