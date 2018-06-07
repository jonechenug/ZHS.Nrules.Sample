using System;
using System.Collections.Generic;
using System.Linq;

namespace ZHS.Nrules.Core.Models.Orders
{
    public class Order:IEntity<String>
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }


        public string BuyerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Address { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public string Id { get; set; }

        public String Remark { get; set; }


        /// <summary>
        /// 获取原来订单的总价
        /// </summary>
        /// <returns></returns>
        public Decimal GetOriginalPrice()
        {
            return SumPrice(OrderItems);
        }

        /// <summary>
        /// 计算总价
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Decimal SumPrice(IEnumerable<OrderItem> items)
        {
            return items.Sum(i => i?.GetOriginalPrice() ?? 0);
        }


        /// <summary>
        /// 获取全部的减免费用
        /// </summary>
        /// <returns></returns>
        public Decimal GetTotalReduction()
        {
            return OrderItems.Sum(i => i?.GetReduction() ?? 0);
        }

        /// <summary>
        /// 获取最终的总价
        /// </summary>
        /// <returns></returns>
        public Decimal GetTotalPrice()
        {
            return GetOriginalPrice() - GetTotalReduction();
        }

        /// <summary>
        /// 获取受影响范围内的原价
        /// </summary>
        /// <param name="productIdRanges"></param>
        /// <returns></returns>
        public Decimal GetRangesOriginalPrice(List<String> productIdRanges)
        {
            return SumPrice(GetRangesItems(productIdRanges));
        }

        /// <summary>
        /// 获取受影响范围内的商品数量
        /// </summary>
        /// <param name="productIdRanges"></param>
        /// <returns></returns>
        public int GetRangesTotalCount(List<String> productIdRanges)
        {
            return GetRangesItems(productIdRanges).Sum(i => i.Quantity);
        }

        /// <summary>
        /// 获取受影响的范围
        /// </summary>
        /// <param name="productIdRanges"></param>
        /// <returns></returns>
        private IEnumerable<OrderItem> GetRangesItems(List<String> productIdRanges)
        {
            if (productIdRanges == null) throw new ArgumentNullException(nameof(productIdRanges));
            return OrderItems.Where(i => productIdRanges.Contains(i.ProductId));
        }


        /// <summary>
        /// 在受影响的订单详情中打折
        /// </summary>
        /// <param name="ranges"></param>
        /// <param name="discount"></param>
        /// <param name="promotionName"></param>
        /// <param name="promotionId"></param>
        public void DiscountOrderItems(List<String> productIdRanges, Decimal discountOff, String promotionName, String promotionId)
        {
            var listRange = GetRangesItems(productIdRanges);
            foreach (var item in listRange)
            {
                item.Promotions.Add(new OrderItemPromotion
                {
                    PromotionName = promotionName,
                    PromotionId = promotionId,
                    Reduction =Math.Round(item.GetOriginalPrice()*discountOff/100, 2)
                });
            }
        }


        public void AddRemark(String remark)
        {
            Remark += remark;
        }

        /// <summary>
        /// 平分减免到受影响的订单详情中
        /// </summary>
        /// <param name="productIdRanges"></param>
        /// <param name="reduction"></param>
        /// <param name="promotionName"></param>
        /// <param name="promotionId"></param>
        public void AverageReduction(List<String> productIdRanges, Decimal reduction, String promotionName,
            String promotionId)
        {
            var listRange = GetRangesItems(productIdRanges);
            AverageReductionRangesItems(listRange,productIdRanges,reduction,promotionName,promotionId);
        }

        /// <summary>
        /// 递归平均减免
        /// </summary>
        /// <param name="items"></param>
        /// <param name="productIdRanges"></param>
        /// <param name="reduction"></param>
        /// <param name="promotionName"></param>
        /// <param name="promotionId"></param>
        private void AverageReductionRangesItems(IEnumerable<OrderItem> items, List<String> productIdRanges,
            Decimal reduction, String promotionName, String promotionId)
        {
            //平分减免,折扣大于价格时，多出部分累计到其他商品
            var count = items.Count(i => i.GetTotalPrice() > 0);
            if (count == 0)
            {
                return;
            }
            else
            {
                var perReduction = Math.Round(reduction / count, 2);
                //小于1分钱则不继续满减
                if (perReduction < 0.01M)
                {
                    return;
                }
                var lastReduction = reduction - perReduction * count;
                foreach (var item in items)
                {
                    var itemReduction = 0M;
                    var itemTotalPrice = item.GetTotalPrice();
                    //判断该项详情的总价是否比平分减免还大
                    if (itemTotalPrice > perReduction)
                    {
                        //如果大，则直接扣减
                        itemReduction = perReduction;
                    }
                    else
                    {
                        //如果小，则最多只能减少到0
                        itemReduction = itemTotalPrice;
                        lastReduction += perReduction - itemTotalPrice;
                    }
                    item.AddPromotion(promotionId,itemReduction,promotionName);
                }
                //剩下的折扣金额
                if (lastReduction != 0)
                {
                    //如果存在最大的超过剩余折扣的，则直接满减
                    var mostLastItem = items.FirstOrDefault(i => i.GetTotalPrice() > lastReduction);
                    if (mostLastItem != null)
                    {
                        mostLastItem.AddPromotion(promotionId,lastReduction,promotionName);
                        return;
                    }
                    //获取折扣后价格为不为0的集合，继续平分减免
                    var luckys = items.Where(i => i.GetTotalPrice() > 0);
                    if (!luckys.Any())
                    {
                        return;
                    }
                    else
                    {
                        //小于1分钱则不继续满减
                        if (GetTotalPrice() <= 0.01m)
                        {
                            return;
                        }
                        AverageReductionRangesItems(luckys, productIdRanges, lastReduction, promotionName, promotionId);
                    }
                }
            }
        }
    }
}