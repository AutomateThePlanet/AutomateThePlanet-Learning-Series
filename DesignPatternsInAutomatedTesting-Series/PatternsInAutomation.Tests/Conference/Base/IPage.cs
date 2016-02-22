using OpenQA.Selenium;

namespace PatternsInAutomation.Tests.Conference
{
    public interface IPage
    {
        void Open(string part = "");
    }
}
