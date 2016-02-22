namespace PatternsInAutomation.Tests.Conference.Pages.BingMain
{
    public interface IBingMainPage : IPage
    {
        void Search(string textToType);
        int GetResultsCount();
    }
}