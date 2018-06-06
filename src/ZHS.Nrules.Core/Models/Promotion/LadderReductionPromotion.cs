using System;
using System.Collections.Generic;

namespace ZHS.Nrules.Core.Models.Promotion
{
    /// <summary>
    /// 阶梯满减
    /// </summary>
    public class LadderReductionPromotion:IBasePromotion
    {
        /// <summary>
        /// 描述阶梯满减的规则
        /// </summary>
        public List<LadderReductionItem> Rules { get; set; }

        public string Name { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public PromotionState State { get; set; }
        public List<string> ProductIdRanges { get; set; }
        public bool IsSingle { get; set; }

        public string Id { get; set; }
    }

    public class LadderReductionItem
    {
        /// <summary>
        /// 最高价
        /// </summary>
        public Decimal BiggestPrice { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public Decimal Reduction { get; set; }
    }

}