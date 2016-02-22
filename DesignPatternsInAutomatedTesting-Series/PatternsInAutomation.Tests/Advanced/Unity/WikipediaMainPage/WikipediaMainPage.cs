using PatternsInAutomation.Tests.Advanced.Unity.Base;

namespace PatternsInAutomation.Tests.Advanced.Unity.WikipediaMainPage
{
    public class WikipediaMainPage : BasePage<WikipediaMainPageMap, WikipediaMainPageValidator>, IWikipediaMainPage
    {
        public WikipediaMainPage()
            : base(@"https://en.wikipedia.org")
        {
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.SearchBox.Click();
        }

        public void ToggleContents()
        {
            this.Map.ContentsToggleLink.Click();
        }
    }
}