using System;
using System.Collections.Generic;
using System.Text;

namespace ZHS.Nrules.Infrastructure.RuleEngine
{
   public interface IExecuterRepository
    {
        void AddRule(RuleDefinition definition);
    }
}
