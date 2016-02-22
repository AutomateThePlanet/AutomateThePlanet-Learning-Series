using PatternsInAutomation.Tests.Advanced.Rules.Data;

namespace PatternsInAutomation.Tests.Advanced.Rules
{
    public class CreditCardChargeRule<TRuleResult> : BaseRule
        where TRuleResult : class, IRuleResult, new()
    {
        private readonly PurchaseTestInput purchaseTestInput;
        private readonly decimal totalPriceLowerBoundary;

        public CreditCardChargeRule(PurchaseTestInput purchaseTestInput, decimal totalPriceLowerBoundary)
        {
            this.purchaseTestInput = purchaseTestInput;
            this.totalPriceLowerBoundary = totalPriceLowerBoundary;
        }

        public override IRuleResult Eval()
        {
            if (!string.IsNullOrEmpty(this.purchaseTestInput.CreditCardNumber) &&
                !this.purchaseTestInput.IsWiretransfer &&
                !this.purchaseTestInput.IsPromotionalPurchase &&
                this.purchaseTestInput.TotalPrice > this.totalPriceLowerBoundary)
            {
                this.ruleResult.IsSuccess = true;
                return this.ruleResult;
            }
            return new TRuleResult();
        }
    }
}