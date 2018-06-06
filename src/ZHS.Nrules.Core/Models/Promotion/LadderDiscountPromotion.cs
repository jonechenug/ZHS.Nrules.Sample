using System;
using System.Collections.Generic;

namespace ZHS.Nrules.Core.Models.Promotion
{
    /// <summary>
    /// 阶梯打折
    /// </summary>
    public class LadderDiscountPromotion :IBasePromotion
    {
        public List<LadderDiscountRuleItem> Rules { get; set; }
        public string Name { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public PromotionState State { get; set; }
        public List<string> ProductIdRanges { get; set; }
        public bool IsSingle { get; set; }
        public string Id { get; set; }
    }

    public class LadderDiscountRuleItem
    {
        /// <summary>
        /// 数量
        /// </summary>
        public Int32 Quantity { get; set; }

        /// <summary>
        /// 打折的百分比
        /// </summary>
        public Decimal DiscountOff { get; set; }
    }
}