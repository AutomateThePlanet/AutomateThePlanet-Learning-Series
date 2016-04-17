using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.NullObject.Base;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage;
using System;

namespace PatternsInAutomatedTests.Advanced.NullObject.Strategies
{
    public class UiPurchasePromotionalCodeStrategy : IPurchasePromotionalCodeStrategy
    {
        private readonly PlaceOrderPage placeOrderPage;
        private readonly double couponDiscountAmount;

        public UiPurchasePromotionalCodeStrategy(PlaceOrderPage placeOrderPage, double couponDiscountAmount)
        {
            this.placeOrderPage = placeOrderPage;
            this.couponDiscountAmount = couponDiscountAmount;
        }

        public void AssertPromotionalCodeDiscount()
        {
            Assert.AreEqual(this.couponDiscountAmount.ToString(), this.placeOrderPage.PromotionalDiscountPrice.Text);
        }

        public double GetPromotionalCodeDiscountAmount()
        {
            return this.couponDiscountAmount;
        }

        public void ApplyPromotionalCode(string couponCode)
        {
            this.placeOrderPage.PromotionalCode.SendKeys(couponCode);
        }
    }
}
