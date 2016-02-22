namespace PatternsInAutomation.Tests.Conference
{
    public interface IItemPage : IPage
    {
        void ClickBuyNowButton();

        double GetPrice();
    }
}