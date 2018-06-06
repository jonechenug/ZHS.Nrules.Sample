using System;
using System.Collections.Generic;

namespace ZHS.Nrules.Core.Models.Promotion
{
    public interface IBasePromotion:IEntity<String>
    {
         String Name { get; set; }

         DateTime StarTime { get; set; }

         DateTime EndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
         PromotionState State { get; set; }

        /// <summary>
        ///  适用商品范围
        /// </summary>
        List<String > ProductIdRanges { get; set; }

        /// <summary>
        /// 是否与其他规则排斥
        /// </summary>
         Boolean IsSingle { get; set; }

    }
}