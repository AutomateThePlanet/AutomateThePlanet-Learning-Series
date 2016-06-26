using HybridTestFramework.UITests.Core;
using System;

namespace HybridTestFramework.UITests.Selenium
{
    public static class ByExtensions
    {
        public static OpenQA.Selenium.By ToSeleniumBy(this Core.By by)
        {
            switch (by.Type)
            {
                case SearchType.Id:
                    return OpenQA.Selenium.By.Id(by.Value);
                case SearchType.Tag:
                    return OpenQA.Selenium.By.TagName(by.Value);
                case SearchType.CssClass:
                    return OpenQA.Selenium.By.ClassName(by.Value);
                case SearchType.XPath:
                    return OpenQA.Selenium.By.XPath(by.Value);
                case SearchType.CssSelector:
                    return OpenQA.Selenium.By.CssSelector(by.Value);
                case SearchType.Name:
                    return OpenQA.Selenium.By.Name(by.Value);
                default:
                    throw new Exception(string.Format("Unknown search type: {0}", by.Type));
            }
        }
    }
}
