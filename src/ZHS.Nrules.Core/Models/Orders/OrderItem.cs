using System;
using System.Collections.Generic;
using System.Linq;

namespace ZHS.Nrules.Core.Models.Orders
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderItem
    {

        public string ProductId { get; set; }

        public String OrderId { get; set; }

        public decimal UnitPrice { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }


        public List<OrderItemPromotion> Promotions { get; set; }

        /// <summary>
        /// 增加参与的优惠
        /// </summary>
        /// <param name="promotionId"></param>
        /// <param name="reduction"></param>
        /// <param name="promotionName"></param>
        public void AddPromotion(String promotionId, Decimal reduction, String promotionName)
        {
            Promotions.Add(new OrderItemPromotion(promotionId, reduction, promotionName));
        }


        /// <summary>
        /// 获取原来的总价
        /// </summary>
        /// <returns></returns>
        public Decimal GetOriginalPrice()
        {
            return Quantity * UnitPrice;
        }

        /// <summary>
        /// 获取总的优惠
        /// </summary>
        /// <returns></returns>
        public Decimal GetReduction()
        {
            if (Promotions != null && Promotions.Count > 0)
            {
                return Promotions.Sum(p => p.Reduction);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取最终的总价
        /// </summary>
        /// <returns></returns>
        public Decimal GetTotalPrice()
        {
            return GetOriginalPrice() - GetReduction();
        }


        public override string ToString()
        {
            return String.Format("Product Id: {0}, Quantity: {1},OriginalPrice:{2},Reduction:{3}",
                ProductId, Quantity, GetOriginalPrice(), GetReduction());
        }

    }
}