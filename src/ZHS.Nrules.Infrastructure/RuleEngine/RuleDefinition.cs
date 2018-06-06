using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZHS.Nrules.Infrastructure.RuleEngine
{
    public class RuleDefinition
    {
        /// <summary>
        /// 规则的名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 约束条件
        /// </summary>
        public List<LambdaExpression> Conditions { get; set; }
        /// <summary>
        ///  执行行动
        /// </summary>
        public  List<LambdaExpression> Actions { get; set; }
        
    }
}