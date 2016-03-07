using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPageSingletonDerived
{
    public class BingMainPage : BasePageSingletonDerived<BingMainPage, BingMainPageElementMap, BingMainPageValidator>
    {
        private BingMainPage() { }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }

        public override void Navigate(string url = "http://www.bing.com/")
        {
            base.Navigate(url);
        }
    }
}