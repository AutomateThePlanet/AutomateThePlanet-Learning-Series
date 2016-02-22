namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public interface IRuleResult
    {
        bool IsSuccess { get; set; }

        void Execute();
    }
}