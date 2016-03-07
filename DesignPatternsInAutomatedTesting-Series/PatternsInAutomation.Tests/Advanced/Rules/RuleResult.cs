using System;
using PatternsInAutomatedTests.Advanced.Rules;

namespace Perfect.SystemTests.Rules
{
    public class RuleResult : IRuleResult
    {
        private readonly Delegate actionToBeExecuted;

        public RuleResult(Delegate actionToBeExecuted)
        {
            this.actionToBeExecuted = actionToBeExecuted;
        }

        public RuleResult()
        {
        }

        public bool IsSuccess { get; set; }

        public void Execute()
        {
            if (this.actionToBeExecuted != null)
            {
                this.actionToBeExecuted.DynamicInvoke();
            }
        }
    }
}