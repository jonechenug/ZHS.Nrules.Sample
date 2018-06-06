using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZHS.Nrules.Core.Models.Orders;
using ZHS.Nrules.Core.Models.Promotion;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.Application.Service
{
    public class RuleEngineService
    {
        private readonly IExecuterRepository _iExecuterRepository;
        private readonly IExecuterContainer _iExecuterContainer;

        public RuleEngineService(IExecuterRepository iExecuterRepository, IExecuterContainer iExecuterContainer)
        {
            _iExecuterRepository = iExecuterRepository;
            _iExecuterContainer = iExecuterContainer;
        }

        public virtual List<RuleDefinition> BuildRuleDefinition(List<IBasePromotion> promotions)
        {
            var result = new List<RuleDefinition>();
            foreach (var p in promotions)
            {
                switch (p)
                {
                    case LadderReductionPromotion ladderReduction:
                        result.AddRange(BuildLadderReductionDefinition(ladderReduction));
                        break;
                    case LadderDiscountPromotion ladderDiscount:
                        result.AddRange(BuildLadderDiscountDefinition(ladderDiscount));
                        break;
                    default:
                        throw new ArgumentException(nameof(p));
                }
            }
            return result;
        }


        /// <summary>
        /// 执行促销活动的规则引擎处理
        /// </summary>
        /// <param name="promotions"></param>
        /// <param name="order"></param>
        public virtual void ExecutePromotion(List<IBasePromotion> promotions,List<Object> facts)
        {
            ExecuteRuleDefinition(BuildRuleDefinition(promotions),facts);
        }


        /// <summary>
        /// 执行规则引擎处理
        /// </summary>
        /// <param name="ruleDefinitions"></param>
        /// <param name="order"></param>
        public virtual void ExecuteRuleDefinition(List<RuleDefinition>  ruleDefinitions,List<Object> facts)
        {
            foreach (var rule in ruleDefinitions)
            {
                _iExecuterRepository.AddRule(rule);
            }

            var session = _iExecuterContainer.CreateSession(_iExecuterRepository);
            foreach (var fact in facts)
            {
                session.Insert(fact);
            }
            session.Fire();
        }







        /// <summary>
        /// 阶梯满减
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        List<RuleDefinition> BuildLadderReductionDefinition(LadderReductionPromotion promotion)
        {
            var ruleDefinitions = new List<RuleDefinition>();
            //按影响的价格倒叙
            var ruleLimits = promotion.Rules.OrderByDescending(r => r.BiggestPrice).ToList();
            var currentIndex = 0;
            var previousLimit = ruleLimits.FirstOrDefault();
            foreach (var current in ruleLimits)
            {
                //约束表达式
                var conditions = new List<LambdaExpression>();
                if (currentIndex == 0)
                {
                    Expression<Func<Order, bool>> conditionPart =
                        o => o.GetRangesOriginalPrice(promotion.ProductIdRanges) >= current.BiggestPrice;
                    conditions.Add(conditionPart);
                }
                else
                {
                    var limit = previousLimit;
                    Expression<Func<Order, bool>> conditionPart =
                        o => current.BiggestPrice <= o.GetRangesOriginalPrice(promotion.ProductIdRanges) &&
                                                                       o.GetRangesOriginalPrice(promotion.ProductIdRanges) < limit.BiggestPrice; ;
                    conditions.Add(conditionPart);
                }
                currentIndex = currentIndex + 1;

                //触发的行为表达式
                var actions = new List<LambdaExpression>();
                Expression<Action<Order>> actionPart = o =>
                    o.AverageReduction(promotion.ProductIdRanges, current.Reduction, promotion.Name, promotion.Id);

                actions.Add(actionPart);

                // 增加描述
                ruleDefinitions.Add(new RuleDefinition
                {
                    Actions = actions,
                    Conditions = conditions,
                    Name = promotion.Name
                });
                previousLimit = current;
            }
            return ruleDefinitions;
        }


        /// <summary>
        /// 阶梯打折
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        List<RuleDefinition> BuildLadderDiscountDefinition(LadderDiscountPromotion promotion)
        {
            var ruleDefinitions = new List<RuleDefinition>();
            //按影响的数量倒叙
            var ruleLimits = promotion.Rules.OrderByDescending(r => r.Quantity).ToList();
            var currentIndex = 0;
            var previousLimit = ruleLimits.FirstOrDefault();
            foreach (var current in ruleLimits)
            {
                //约束表达式
                var conditions = new List<LambdaExpression>();
                var actions = new List<LambdaExpression>();
                if (currentIndex == 0)
                {
                    Expression<Func<Order, bool>> conditionPart =
                        o => o.GetRangesTotalCount(promotion.ProductIdRanges) >= current.Quantity;
                    conditions.Add(conditionPart);
                }
                else
                {
                    var limit = previousLimit;
                    Expression<Func<Order, bool>> conditionPart = o =>
                        o.GetRangesTotalCount(promotion.ProductIdRanges) >= current.Quantity
                        && o.GetRangesTotalCount(promotion.ProductIdRanges) < limit.Quantity;
                    conditions.Add(conditionPart);
                }
                currentIndex = currentIndex + 1;

                //触发的行为表达式
                Expression<Action<Order>> actionPart =
                    o => o.DiscountOrderItems(promotion.ProductIdRanges, current.DiscountOff, promotion.Name, promotion.Id);
                actions.Add(actionPart);

                // 增加描述
                ruleDefinitions.Add(new RuleDefinition
                {
                    Actions = actions,
                    Conditions = conditions,
                    Name = promotion.Name
                });
                previousLimit = current;
            }
            return ruleDefinitions;
        }


    }
}