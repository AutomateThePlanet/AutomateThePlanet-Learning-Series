using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.PageObjectv20.Base
{
    public interface IPage
    {
        void Open(string part = "");
    }
}
