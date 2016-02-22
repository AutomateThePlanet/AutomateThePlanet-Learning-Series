using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Specifications.Base;
using PatternsInAutomation.Tests.Advanced.Specifications.Data;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public partial class LinqPlaceOrderPage : BasePage
    {
        private readonly PurchaseTestInput purchaseTestInput;
        private readonly ISpecification<PurchaseTestInput> promotionalPurchaseSpecification;
        private readonly ISpecification<PurchaseTestInput> creditCardSpecification;
        private readonly ISpecification<PurchaseTestInput> wiretransferSpecification;
        private readonly ISpecification<PurchaseTestInput> freePurchaseSpecification;

        public LinqPlaceOrderPage(IWebDriver driver, PurchaseTestInput purchaseTestInput)
            : base(driver)
        {
            this.purchaseTestInput = purchaseTestInput;
            this.creditCardSpecification = new ExpressionSpecification<PurchaseTestInput>(x => !string.IsNullOrEmpty(x.CreditCardNumber));
            this.freePurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.TotalPrice == 0);
            this.wiretransferSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsWiretransfer);
            this.promotionalPurchaseSpecification = new ExpressionSpecification<PurchaseTestInput>(x => x.IsPromotionalPurchase && x.TotalPrice < 5);
        }

        public override string Url
        {
            get
            {
                return @"http://www.bing.com/";
            }
        }

        public void ChoosePaymentMethod()
        {
            if (this.creditCardSpecification.
            And(this.wiretransferSpecification.Not()).
            And(this.freePurchaseSpecification.Not()).
            And(this.promotionalPurchaseSpecification.Not()).
            IsSatisfiedBy(this.purchaseTestInput))
            {
                this.CreditCard.SendKeys("371449635398431");
                this.SecurityNumber.SendKeys("1234");
            }
            else
            {
                this.Wiretransfer.SendKeys("pathToFile");
            }
        }
    }
}