namespace PatternsInAutomatedTests.Advanced.NullObject.Base
{
    public interface IPurchasePromotionalCodeStrategy
    {
        void AssertPromotionalCodeDiscount();

        double GetPromotionalCodeDiscountAmount();

        void ApplyPromotionalCode(string couponCode);
    }
}
