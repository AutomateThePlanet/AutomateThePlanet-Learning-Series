using PatternsInAutomation.Tests.Advanced.PageObjectv20.Base;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv20
{
    public interface IBingMainPage : IPage
    {
        void Search(string textToType);

        int GetResultsCount();
    }
}