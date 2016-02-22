using PatternsInAutomation.Tests.Advanced.Specifications.Data;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class OrderTestContext
    {
        public OrderTestContext(PurchaseTestInput purchaseTestInput, OrderTestContextConfigurator orderTestContextConfigurator)
        {
            this.PurchaseTestInput = purchaseTestInput;
            this.IsPromoCodePurchase = orderTestContextConfigurator.FreePurchaseSpecification.
                Or(orderTestContextConfigurator.PromotionalPurchaseSpecification).
                IsSatisfiedBy(purchaseTestInput);
            this.IsCreditCardPurchase =
                orderTestContextConfigurator.CreditCardSpecification.
                And(orderTestContextConfigurator.WiretransferSpecification.Not()).
                And(orderTestContextConfigurator.FreePurchaseSpecification.Not()).
                And(orderTestContextConfigurator.PromotionalPurchaseSpecification.Not()).
                IsSatisfiedBy(purchaseTestInput);
        }

        public PurchaseTestInput PurchaseTestInput { get; private set; }

        public bool IsPromoCodePurchase { get; private set; }

        public bool IsCreditCardPurchase { get; private set; }
    }
}