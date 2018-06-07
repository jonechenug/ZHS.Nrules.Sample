using System;
using NRules;
using ZHS.Nrules.Infrastructure.RuleEngine;

namespace ZHS.Nrules.RuleEngine
{
    public class NulesExecuterContainer: IExecuterContainer
    {
        public IExecuterSession CreateSession(IExecuterRepository executerRepository)
        {
           return CreateSession(executerRepository,null);
        }

        public IExecuterSession CreateSession(IExecuterRepository executerRepository,Action<IExecuterSession> initializationAction)
        {
            var repository = executerRepository as ExecuterRepository;
            repository.LoadAssemblys();
            ISessionFactory factory = repository.Compile();
            var session = new NulesExecuterSession(factory.CreateSession());
            if (initializationAction!=null)
            {
                initializationAction(session);
            }
            return session;
        }
    }
}