namespace PatternsInAutomatedTests.Advanced.Unity.WikipediaMainPage
{
    public interface IWikipediaMainPage
    {
        void Navigate(string part = "");

        WikipediaMainPageValidator Validate();

        void Search(string textToType);

        void ToggleContents();
    }
}