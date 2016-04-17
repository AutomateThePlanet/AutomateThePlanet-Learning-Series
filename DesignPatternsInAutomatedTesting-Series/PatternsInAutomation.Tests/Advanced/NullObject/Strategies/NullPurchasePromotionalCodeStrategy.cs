using PatternsInAutomatedTests.Advanced.NullObject.Base;
using System;

namespace PatternsInAutomatedTests.Advanced.NullObject.Strategies
{
    public class NullPurchasePromotionalCodeStrategy : IPurchasePromotionalCodeStrategy
    {
        public void AssertPromotionalCodeDiscount()
        {
        }

        public double GetPromotionalCodeDiscountAmount()
        {
            return 0;
        }

        public void ApplyPromotionalCode(string couponCode)
        {
        }
    }
}