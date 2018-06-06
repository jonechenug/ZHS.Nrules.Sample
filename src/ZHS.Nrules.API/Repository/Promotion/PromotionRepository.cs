using System;
using System.Collections.Generic;
using ZHS.Nrules.Core.Models.Promotion;
using ZHS.Nrules.Infrastructure.Repository;
using ZHS.Nrules.Infrastructure.Util;

namespace ZHS.Nrules.API.Repository.Promotion
{
    public class PromotionRepository : BaseFakeRepository<IBasePromotion, String>, IBasePromotionRepository
    {
        public PromotionRepository()
        {

            this.Add(new LadderDiscountPromotion
            {
                Id ="1",
                Name = "非独斥阶梯打折",
                StarTime = DateTime.Now.AddDays(-1),               
                EndTime = DateTime.Now.AddDays(1),
                Rules = new List<LadderDiscountRuleItem>
                {
                    new LadderDiscountRuleItem{Quantity = 1,DiscountOff = 20},
                    new LadderDiscountRuleItem{Quantity = 2,DiscountOff = 40}
                },
                ProductIdRanges = new List<string>{"1"},
                State = PromotionState.Actived,
                IsSingle = false,
            });

            this.Add(new LadderReductionPromotion
            {
                Id ="2",
                Name = "非独斥阶梯满减",
                StarTime = DateTime.Now.AddDays(-1),               
                EndTime = DateTime.Now.AddDays(1),
                Rules =new List<LadderReductionItem>
                {
                    new LadderReductionItem
                    {
                        BiggestPrice = 100,
                        Reduction = 10
                    },
                    new LadderReductionItem
                    {
                        BiggestPrice = 200,
                        Reduction = 30
                    },
                },
                State = PromotionState.Actived,
                ProductIdRanges = new List<string>{"1"},
                IsSingle = false,
            });

            this.Add(new LadderDiscountPromotion
            {
                Id ="3",
                Name = "独斥阶梯打折",
                StarTime = DateTime.Now.AddDays(-1),               
                EndTime = DateTime.Now.AddDays(1),
                Rules = new List<LadderDiscountRuleItem>
                {
                    new LadderDiscountRuleItem{Quantity = 1,DiscountOff = 50},
                },
                State = PromotionState.Actived,
                ProductIdRanges = new List<string>{"1"},
                IsSingle = true,
            });
        }
    }
}