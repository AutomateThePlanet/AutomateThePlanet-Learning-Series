using PatternsInAutomation.Tests.Advanced.Core.Fluent;
using PatternsInAutomation.Tests.Advanced.Fluent.Enums;

namespace PatternsInAutomation.Tests.Advanced.Fluent.BingMainPage
{
    public class BingMainPage : BaseFluentPageSingleton<BingMainPage, BingMainPageElementMap, BingMainPageValidator>
    {
        public BingMainPage Navigate(string url = "http://www.bing.com/")
        {
            base.Navigate(url);
            return this;
        }

        public BingMainPage Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
            return this;
        }

        public BingMainPage ClickImages()
        {
            this.Map.ImagesLink.Click();
            return this;
        }

        public BingMainPage SetSize(Sizes size)
        {
            this.Map.Sizes.SelectByIndex((int)size);
            return this;
        }

        public BingMainPage SetColor(Colors color)
        {
            this.Map.Color.SelectByIndex((int)color);
            return this;
        }

        public BingMainPage SetTypes(Types type)
        {
            this.Map.Type.SelectByIndex((int)type);
            return this;
        }

        public BingMainPage SetLayout(Layouts layout)
        {
            this.Map.Layout.SelectByIndex((int)layout);
            return this;
        }

        public BingMainPage SetPeople(People people)
        {
            this.Map.People.SelectByIndex((int)people);
            return this;
        }

        public BingMainPage SetDate(Dates date)
        {
            this.Map.Date.SelectByIndex((int)date);
            return this;
        }

        public BingMainPage SetLicense(Licenses license)
        {
            this.Map.License.SelectByIndex((int)license);
            return this;
        }
    }
}