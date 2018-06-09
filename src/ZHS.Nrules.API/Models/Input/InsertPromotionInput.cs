using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZHS.Nrules.Core.Models.Orders;
using ZHS.Nrules.Core.Models.Promotion;

namespace ZHS.Nrules.API.Models.Output
{
    public class InsertPromotionInput
    {
        public JObject Promotion { get; set; }
        public InsertPromotionInputTypeName? TypeName { get; set; }
    }

    public enum InsertPromotionInputTypeName
    {
        LadderDiscountPromotion,LadderReductionPromotion
    }
}