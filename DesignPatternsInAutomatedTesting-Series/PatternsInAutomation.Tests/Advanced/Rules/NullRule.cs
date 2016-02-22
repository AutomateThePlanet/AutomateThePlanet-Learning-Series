
namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public class NullRule : BaseRule
    {
        public NullRule(System.Action actionToBeExecuted) : base(actionToBeExecuted)
        {
        }

        public override IRuleResult Eval()
        {
            this.ruleResult.IsSuccess = true;
            return ruleResult;
        }
    }
}