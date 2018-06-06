using System;
using System.Collections.Generic;
using System.Text;

namespace ZHS.Nrules.Infrastructure.RuleEngine
{
    /// <summary>
    /// 执行器的会话
    /// </summary>
    public interface IExecuterSession
    {
        /// <summary>
        /// 插入需要处理的对象
        /// </summary>
        /// <param name="fact"></param>
        void Insert(object fact);

        /// <summary>
        /// 插入需要处理的对象
        /// </summary>
        /// <param name="fact"></param>
        void InsertAll(IEnumerable<object> facts);

        /// <summary>
        /// 执行，返回命中的规则数目
        /// </summary>
        /// <returns></returns>
        int Fire();
    }
}
