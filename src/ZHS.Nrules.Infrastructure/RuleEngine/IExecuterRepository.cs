using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ZHS.Nrules.Infrastructure.RuleEngine
{
   public interface IExecuterRepository
    {
        /// <summary>
        /// 加载运行时的规则
        /// </summary>
        /// <param name="definition"></param>
        void AddRule(RuleDefinition definition);
        /// <summary>
        /// 加载预设的规则
        /// </summary>
        /// <param name="assemblys"></param>
        void AddAssembly(IEnumerable<Assembly> assemblys);
        /// <summary>
        ///  加载预设的规则
        /// </summary>
        /// <param name="assembly"></param>
        void AddAssembly(Assembly assembly);
    }
}
