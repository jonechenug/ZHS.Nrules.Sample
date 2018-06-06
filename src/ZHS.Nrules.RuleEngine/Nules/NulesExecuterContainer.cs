using System;
using NRules;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.RuleEngine
{
    public class NulesExecuterContainer: IExecuterContainer
    {
        public IExecuterSession CreateSession(IExecuterRepository executerRepository)
        {
            ISessionFactory factory = (executerRepository as ExecuterRepository).Compile();
            return new NulesExecuterSession(factory.CreateSession());
        }

        public IExecuterSession CreateSession(IExecuterRepository executerRepository,Action<IExecuterSession> initializationAction)
        {
            ISessionFactory factory =  (executerRepository as ExecuterRepository).Compile();
            var session = new NulesExecuterSession(factory.CreateSession());
            initializationAction(session);
            return session;
        }
    }
}