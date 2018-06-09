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

namespace ZHS.Nrules.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly PromotionService _promotionService;

        public OrderController(OrderService orderService,
            PromotionService promotionService,
            RuleEngineService ruleEngineService)
        {
            _orderService = orderService;
            _promotionService = promotionService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Order> All()
        {
            return _orderService.QueryAble().ToList();
        }

        [AllowAnonymous]
        [HttpPost]
        public Order InserOrUpdat(Order o)
        {
            return _orderService.InsertOrUpdate(o);
        }


        /// <summary>
        /// 获取受促销引擎影响所有的返回结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<AllPromotionForOrderOutput> AllPromotionForOrder([FromQuery]String id)
        {
            var result = new List<AllPromotionForOrderOutput>();
            var order = _orderService.Get(id) ?? throw new ArgumentNullException("_orderService.Get(id)");
            var promotionGroup = _promotionService.GetActiveGroup();
            var orderjson = JsonConvert.SerializeObject(order);
            foreach (var promotions in promotionGroup)
            {
                var tempOrder = JsonConvert.DeserializeObject<Order>(orderjson);
                var ruleEngineService = HttpContext.RequestServices.GetService(typeof(RuleEngineService)) as RuleEngineService;
                ruleEngineService.AddAssembly(typeof(OrderRemarkRule).Assembly);
                ruleEngineService.ExecutePromotion(promotions, new List<object>
                {
                    tempOrder
                });
                result.Add(new AllPromotionForOrderOutput(tempOrder));
            }
            return result.OrderBy(i => i.Order.GetTotalPrice());
        }
    }
}