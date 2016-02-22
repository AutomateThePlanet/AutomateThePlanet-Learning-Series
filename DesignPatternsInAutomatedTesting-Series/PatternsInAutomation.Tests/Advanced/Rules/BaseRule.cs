using System;
using Perfect.SystemTests.Rules;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public abstract class BaseRule : IRule
    {
        private readonly Action actionToBeExecuted;
        protected readonly RuleResult ruleResult;

        public BaseRule(Action actionToBeExecuted)
        {
            this.actionToBeExecuted = actionToBeExecuted;
            if (actionToBeExecuted != null)
            {
                this.ruleResult = new RuleResult(this.actionToBeExecuted);
            }
            else
            {
                this.ruleResult = new RuleResult();
            }
        }

        public BaseRule()
        {
            ruleResult = new RuleResult();
        }

        public abstract IRuleResult Eval();
    }
}