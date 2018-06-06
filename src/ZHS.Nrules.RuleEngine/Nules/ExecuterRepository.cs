using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NRules.RuleModel;
using NRules.RuleModel.Builders;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.RuleEngine
{
    public class ExecuterRepository:IExecuterRepository, IRuleRepository
    {
        private readonly IRuleSet _ruleSet = new RuleSet("MyRuleSet");

        public IEnumerable<IRuleSet> GetRuleSets()
        {
            return new[] { _ruleSet };
        }

        public void AddRule(RuleDefinition definition)
        {
            var builder = new RuleBuilder();
            builder.Name(definition.Name);
            foreach (var condition in definition.Conditions)
            {
                ParsePattern(builder, condition);
            }
            foreach (var action in definition.Actions)
            {
                var param = action.Parameters.FirstOrDefault();
                var obj = GetObject(param.Type);
                builder.RightHandSide().Action(ParseAction(obj, action, param.Name));
            }
            _ruleSet.Add(new[] { builder.Build() });
        }


        PatternBuilder ParsePattern(RuleBuilder builder, LambdaExpression condition)
        {
            var parameter = condition.Parameters.FirstOrDefault();
            var type = parameter.Type;
            var customerPattern = builder.LeftHandSide().Pattern(type, parameter.Name);
            customerPattern.Condition(condition);
            return customerPattern;
        }


        LambdaExpression ParseAction<TEntity>(TEntity entity, LambdaExpression action, String param) where TEntity : class, new()
        {
            return NRulesHelper.AddContext(action as Expression<Action<TEntity>>);
        }

        static dynamic GetObject(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}