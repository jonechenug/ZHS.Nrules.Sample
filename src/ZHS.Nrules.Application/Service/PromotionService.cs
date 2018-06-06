using System;
using System.Collections.Generic;
using System.Linq;
using ZHS.Nrules.Core.Models.Promotion;
using ZHS.Nrules.Infrastructure.Repository;

namespace ZHS.Nrules.Application.Service
{
    public class PromotionService
    {
        private readonly IBasePromotionRepository _promotionRepository;

        public PromotionService(IBasePromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        /// <summary>
        /// 获取生效中的促销活动配置
        /// </summary>
        /// <returns></returns>
        public List<List<IBasePromotion>> GetActiveGroup()
        {
            var result = new List<List<IBasePromotion>>();
            var allActiveList = _promotionRepository.FindList(i =>
                i.StarTime <= DateTime.Now && i.EndTime >= DateTime.Now && i.State == PromotionState.Actived);
            //非独斥的为一组
            var noSingleList = allActiveList.Where(i => !i.IsSingle);
            result.Add(noSingleList.ToList());
            //独斥的单独为一组
            var singleList = allActiveList.Where(i => i.IsSingle);
            foreach (var item in singleList)
            {
                result.Add(new List<IBasePromotion>
                {
                    item
                });
            }
            return result;
        }

    }
}