using PatternsInAutomation.Tests.Advanced.Specifications.Data;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class OrderTestContextConfigurator
    {
        public OrderTestContextConfigurator()
        {
            this.CreditCardSpecification = new ExpressionSpecification<PurchaseTestInput>(x => !string.IsNullOrEmpty(x.CreditCardNumber));
            this.FreePurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.TotalPrice == 0);
            this.WiretransferSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsWiretransfer);
            this.PromotionalPurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsPromotionalPurchase && x.TotalPrice < 5);
        }

        public ISpecification<PurchaseTestInput> PromotionalPurchaseSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> CreditCardSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> WiretransferSpecification { get; private set; }

        public ISpecification<PurchaseTestInput> FreePurchaseSpecification { get; private set; }
    }
}