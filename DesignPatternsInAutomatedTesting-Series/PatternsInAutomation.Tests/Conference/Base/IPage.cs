using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Conference
{
    public interface IPage
    {
        void Open(string part = "");
    }
}
