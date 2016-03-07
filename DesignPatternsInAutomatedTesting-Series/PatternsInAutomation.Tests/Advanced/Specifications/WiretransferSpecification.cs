using PatternsInAutomatedTests.Advanced.Specifications.Data;
using System;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public class WiretransferSpecification : Specification<PurchaseTestInput>
    {
        private readonly PurchaseTestInput purchaseTestInput;

        public WiretransferSpecification(PurchaseTestInput purchaseTestInput)
        {
            this.purchaseTestInput = purchaseTestInput;
        }

        public override bool IsSatisfiedBy(PurchaseTestInput entity)
        {
            return this.purchaseTestInput.IsWiretransfer;
        }
    }
}