using System;
using System.Linq;
using System.Linq.Expressions;
using NRules.RuleModel;

namespace ZHS.Nrules.RuleEngine
{
    internal class NRulesHelper
    {
        internal static Expression<Action<IContext>> AddContext(Expression<Action> action)
        {
            var convertedParameters = new[] {Expression.Parameter(typeof(IContext))};
            var result = Expression.Lambda<Action<IContext>>(Expression.Invoke(action, action.Parameters), convertedParameters);
            return result;
        }

        internal static Expression<Action<IContext, T>> AddContext<T>(Expression<Action<T>> action)
        {
            var convertedParameters = new[] {Expression.Parameter(typeof(IContext))}
                .Concat(action.Parameters).ToArray();
            var result = Expression.Lambda<Action<IContext, T>>(action.Body, convertedParameters);
            return result;
        }

        internal static Expression<Action<IContext, T1, T2>> AddContext<T1, T2>(Expression<Action<T1, T2>> action)
        {
            var convertedParameters = new[] {Expression.Parameter(typeof(IContext))}
                .Concat(action.Parameters).ToArray();
            var result = Expression.Lambda<Action<IContext, T1, T2>>(action.Body, convertedParameters);
            return result;
        }
    }
}