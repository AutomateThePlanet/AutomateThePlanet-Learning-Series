using System;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public interface IRule
    {
        IRuleResult Eval();
    }
}