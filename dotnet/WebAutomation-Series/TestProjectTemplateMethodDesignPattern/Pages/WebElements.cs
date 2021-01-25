using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProjectTemplateMethodDesignPattern.Pages
{
    public abstract class WebElements
    {
        protected readonly IWebDriver Driver;

        protected WebElements(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement WaitToBeClickable(By by)
        {
            var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
