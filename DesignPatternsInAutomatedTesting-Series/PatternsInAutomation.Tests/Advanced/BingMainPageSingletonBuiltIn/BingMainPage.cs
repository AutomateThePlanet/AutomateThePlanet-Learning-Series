using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPageSingletonBuiltIn
{
    public class BingMainPage : BasePage<PatternsInAutomatedTests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageElementMap, PatternsInAutomatedTests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageValidator>
    {
        public BingMainPage()
           : base(@"http://www.bing.com/")
        {
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }
    }
}