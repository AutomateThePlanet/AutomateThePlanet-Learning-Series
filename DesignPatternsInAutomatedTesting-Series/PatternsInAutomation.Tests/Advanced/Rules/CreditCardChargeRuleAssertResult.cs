using System;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public class CreditCardChargeRuleAssertResult : IRuleResult
    {
        public bool IsSuccess { get; set; }

        public void Execute()
        {
            Console.WriteLine("Perform DB asserts.");
        }
    }
}