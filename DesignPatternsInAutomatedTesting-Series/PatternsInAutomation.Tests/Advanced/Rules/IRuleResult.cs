namespace PatternsInAutomatedTests.Advanced.Rules
{
    public interface IRuleResult
    {
        bool IsSuccess { get; set; }

        void Execute();
    }
}