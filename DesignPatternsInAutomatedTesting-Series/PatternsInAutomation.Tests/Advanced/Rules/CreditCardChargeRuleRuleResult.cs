using System;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public class CreditCardChargeRuleRuleResult : IRuleResult
    {
        public bool IsSuccess { get; set; }

        public void Execute()
        {
            Console.WriteLine("Assert that total amount label is over specified + additional UI actions");
        }
    }
}