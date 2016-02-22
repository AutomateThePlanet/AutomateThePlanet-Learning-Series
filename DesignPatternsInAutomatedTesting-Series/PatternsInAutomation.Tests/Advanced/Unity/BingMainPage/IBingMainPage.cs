using PatternsInAutomation.Tests.Advanced.Unity.Enums;

namespace PatternsInAutomation.Tests.Advanced.Unity.BingMainPage
{
    public interface IBingMainPage
    {
        void Navigate(string part = "");
        BingMainPageValidator Validate();
        void Search(string textToType);
        void ClickImages();
        void SetSize(Sizes size);
        void SetColor(Colors color);
        void SetTypes(Types type);
        void SetLayout(Layouts layout);
        void SetPeople(People people);
        void SetDate(Dates date);
        void SetLicense(Licenses license);
    }
}
