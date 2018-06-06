using System.Collections.Generic;
using NRules;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.RuleEngine
{
    public class NulesExecuterSession: IExecuterSession
    {
        private readonly ISession _session;

        public NulesExecuterSession(ISession session)
        {
            _session = session;
        }

        public void Insert(object fact)
        {
            _session.Insert(fact);
        }

        public void InsertAll(IEnumerable<object> facts)
        {
            _session.Insert(facts);
        }

        public int Fire()
        {
            return _session.Fire();
        }
    }
}