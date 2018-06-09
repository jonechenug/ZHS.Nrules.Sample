using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZHS.Nrules.Application.Service;
using ZHS.Nrules.API.Models.Output;
using ZHS.Nrules.API.Rule;
using ZHS.Nrules.Core.Models.Orders;
using ZHS.Nrules.Core.Models.Promotion;

namespace ZHS.Nrules.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PromotionController : Controller
    {
        private readonly PromotionService _promotionService;

        public PromotionController(PromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<IBasePromotion> All()
        {
            return _promotionService.QueryAble().ToList();
        }

        [AllowAnonymous]
        [HttpPost]
        public IBasePromotion InsertOrUpdate(InsertPromotionInput t)
        {
            if (t.TypeName==null)
            {
                throw new ArgumentException(nameof(t.TypeName));
            }
            switch(t.TypeName){
                case InsertPromotionInputTypeName.LadderDiscountPromotion:
                return _promotionService.InsertOrUpdate(t.Promotion.ToObject<LadderDiscountPromotion>());
                case InsertPromotionInputTypeName.LadderReductionPromotion:
                return _promotionService.InsertOrUpdate(t.Promotion.ToObject<LadderReductionPromotion>());
                default:
                throw new ArgumentException(nameof(t.TypeName));
            }
        }
    }
}