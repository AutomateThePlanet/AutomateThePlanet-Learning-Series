using System;

namespace PatternsInAutomatedTests.Advanced.Rules
{
    public interface IRule
    {
        IRuleResult Eval();
    }
}