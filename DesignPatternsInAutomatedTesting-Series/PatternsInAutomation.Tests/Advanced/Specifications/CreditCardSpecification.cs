using PatternsInAutomation.Tests.Advanced.Specifications.Data;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class CreditCardSpecification : Specification<PurchaseTestInput>
    {
        private readonly PurchaseTestInput purchaseTestInput;

        public CreditCardSpecification(PurchaseTestInput purchaseTestInput)
        {
            this.purchaseTestInput = purchaseTestInput;
        }  

        public override bool IsSatisfiedBy(PurchaseTestInput entity)
        {
            return !string.IsNullOrEmpty(this.purchaseTestInput.CreditCardNumber);
        }
    }
}