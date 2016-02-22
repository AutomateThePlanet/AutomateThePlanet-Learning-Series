using OpenQA.Selenium;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv20.Base
{
    public interface IPage
    {
        void Open(string part = "");
    }
}
