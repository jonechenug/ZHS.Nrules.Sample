using System;

namespace ZHS.Nrules.Infrastructure.RuleEngine
{
    public interface IExecuterContainer
    {
        IExecuterSession CreateSession(IExecuterRepository executerRepository);

        IExecuterSession CreateSession(IExecuterRepository executerRepository,Action<IExecuterSession> initializationAction);

    }
}