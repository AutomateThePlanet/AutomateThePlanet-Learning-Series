using PatternsInAutomation.Tests.Advanced.Unity.Base;
using PatternsInAutomation.Tests.Advanced.Unity.Enums;

namespace PatternsInAutomation.Tests.Advanced.Unity.BingMainPage
{
    public class BingMainPage : BasePage<BingMainPageElementMap, BingMainPageValidator>, IBingMainPage
    {
        public BingMainPage() : base(@"http://www.bing.com/")
        {
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }

        public void ClickImages()
        {
            this.Map.ImagesLink.Click();
        }

        public void SetSize(Sizes size)
        {
            this.Map.Sizes.SelectByIndex((int)size);
        }

        public void SetColor(Colors color)
        {
            this.Map.Color.SelectByIndex((int)color);
        }

        public void SetTypes(Types type)
        {
            this.Map.Type.SelectByIndex((int)type);
        }

        public void SetLayout(Layouts layout)
        {
            this.Map.Layout.SelectByIndex((int)layout);
        }

        public void SetPeople(People people)
        {
            this.Map.People.SelectByIndex((int)people);
        }

        public void SetDate(Dates date)
        {
            this.Map.Date.SelectByIndex((int)date);
        }

        public void SetLicense(Licenses license)
        {
            this.Map.License.SelectByIndex((int)license);
        }
    }
}