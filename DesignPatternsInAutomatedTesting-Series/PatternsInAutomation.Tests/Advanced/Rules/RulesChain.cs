using System;
using System.Collections.Generic;
using System.Linq;
using PatternsInAutomatedTests.Advanced.Rules;

namespace PatternsInAutomatedTests.Advanced.Rules
{
    public class RulesChain
    {
        public IRule Rule { get; set; }

        public List<RulesChain> ElseRules { get; set; }

        public bool IsLastInChain { get; set; }

        public RulesChain(IRule mainRule, bool isLastInChain = false)
        {
            this.IsLastInChain = isLastInChain;
            this.ElseRules = new List<RulesChain>();
            this.Rule = mainRule;
        }
    }
}