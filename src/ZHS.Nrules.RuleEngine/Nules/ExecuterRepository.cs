using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NRules.Fluent;
using NRules.RuleModel;
using NRules.RuleModel.Builders;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.RuleEngine
{
    public class ExecuterRepository : IRuleRepository, IExecuterRepository
    {
        private readonly RuleRepository _internalRuleRepository;

        private readonly IRuleSet _ruleSet;

        private List<Assembly> _assemblys;

        public ExecuterRepository()
        {
            _internalRuleRepository = new RuleRepository();
            _ruleSet = new RuleSet("default");
            _assemblys = new List<Assembly>();
        }

        public IEnumerable<IRuleSet> GetRuleSets()
        {
            //合并
            var sets = new List<IRuleSet>();
            sets.Add(_ruleSet);
            sets.AddRange(_internalRuleRepository.GetRuleSets());
            return sets;
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


        public void AddAssembly(IEnumerable<Assembly> assemblys)
        {
            _assemblys.AddRange(assemblys);
        }

        public void AddAssembly(Assembly assembly)
        {
            _assemblys.Add(assembly);
        }

        internal void LoadAssemblys()
        {
            _internalRuleRepository.Load(x => x.From(_assemblys));
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
            return System.Activator.CreateInstance(type);
        }
    }
}