using System;

namespace ZHS.Nrules.Core.Models.Orders
{
    /// <summary>
    /// 订单详情参与的优惠
    /// </summary>
    public class OrderItemPromotion
    {
        public String PromotionId { get; set; }

        public Decimal Reduction { get; set; }

        public String PromotionName { get; set; }

        public OrderItemPromotion()
        {
        }

        public OrderItemPromotion(String promotionId,Decimal reduction,String promotionName)
        {
            PromotionId = promotionId;
            Reduction = reduction;
            PromotionName = promotionName;
        }
    }
}