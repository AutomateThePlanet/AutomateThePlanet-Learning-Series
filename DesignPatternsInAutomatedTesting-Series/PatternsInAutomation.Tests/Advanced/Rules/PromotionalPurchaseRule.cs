using PatternsInAutomation.Tests.Advanced.Rules.Data;
using Perfect.SystemTests.Rules;
using System;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public class PromotionalPurchaseRule : BaseRule
    {
        private readonly PurchaseTestInput purchaseTestInput;

        public PromotionalPurchaseRule(PurchaseTestInput purchaseTestInput, Action actionToBeExecuted) : base(actionToBeExecuted)
        {
            this.purchaseTestInput = purchaseTestInput;
        }

        public override IRuleResult Eval()
        {
            if (string.IsNullOrEmpty(this.purchaseTestInput.CreditCardNumber) &&
                !this.purchaseTestInput.IsWiretransfer &&                
                this.purchaseTestInput.IsPromotionalPurchase &&
                this.purchaseTestInput.TotalPrice == 0)
            {
                this.ruleResult.IsSuccess = true;
                return this.ruleResult;
            }
            return new RuleResult();
        }
    }
}