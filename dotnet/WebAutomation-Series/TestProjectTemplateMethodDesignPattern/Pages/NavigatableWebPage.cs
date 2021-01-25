using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProjectTemplateMethodDesignPattern.Pages
{
    public abstract class NavigatableWebPage<TElements> : WebPage<TElements>
        where TElements : WebElements
    {
        private WebDriverWait _webDriverWait;

        protected NavigatableWebPage(IWebDriver driver)
            : base(driver)
        {
            _webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        protected abstract string Url { get; }

        public TPage Open<TPage>()
        {
            Driver.Navigate().GoToUrl(Url);
            WaitForPageLoad();
            return (TPage)Activator.CreateInstance(typeof(TPage), new object[] { Driver });
        }

        protected void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)Driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        protected void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)Driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        protected void WaitForElementToExists(By by)
        {
            var js = (IJavaScriptExecutor)Driver;
            _webDriverWait.Until(ExpectedConditions.ElementExists(by));
        }

        protected abstract void WaitForPageLoad();
    }
}
