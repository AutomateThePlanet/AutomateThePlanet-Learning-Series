using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.BingMainPageSingletonBuiltIn
{
    public class BingMainPage : BasePage<PatternsInAutomation.Tests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageElementMap, PatternsInAutomation.Tests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageValidator>
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