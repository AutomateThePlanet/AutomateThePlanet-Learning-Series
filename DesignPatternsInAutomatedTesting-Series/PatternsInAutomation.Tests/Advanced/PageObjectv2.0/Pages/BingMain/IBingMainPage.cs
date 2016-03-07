using PatternsInAutomatedTests.Advanced.PageObjectv20.Base;

namespace PatternsInAutomatedTests.Advanced.PageObjectv20
{
    public interface IBingMainPage : IPage
    {
        void Search(string textToType);

        int GetResultsCount();
    }
}