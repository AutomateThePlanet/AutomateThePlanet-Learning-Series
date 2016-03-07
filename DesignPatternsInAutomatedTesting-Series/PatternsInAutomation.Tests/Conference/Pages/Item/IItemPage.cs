namespace PatternsInAutomatedTests.Conference
{
    public interface IItemPage : IPage
    {
        void ClickBuyNowButton();

        double GetPrice();
    }
}