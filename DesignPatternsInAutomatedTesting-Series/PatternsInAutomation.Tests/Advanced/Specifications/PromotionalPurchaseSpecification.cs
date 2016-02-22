using PatternsInAutomation.Tests.Advanced.Specifications.Data;
using System;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class PromotionalPurchaseSpecification : Specification<PurchaseTestInput>
    {
        private readonly PurchaseTestInput purchaseTestInput;

        public PromotionalPurchaseSpecification(PurchaseTestInput purchaseTestInput)
        {
            this.purchaseTestInput = purchaseTestInput;
        }

        public override bool IsSatisfiedBy(PurchaseTestInput entity)
        {
            return this.purchaseTestInput.IsPromotionalPurchase && this.purchaseTestInput.TotalPrice < 5;
        }
    }
}