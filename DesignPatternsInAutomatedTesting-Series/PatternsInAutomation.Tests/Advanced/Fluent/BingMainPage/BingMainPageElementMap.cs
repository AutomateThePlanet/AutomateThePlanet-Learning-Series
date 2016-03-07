using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PatternsInAutomatedTests.Advanced.Core.Fluent;

namespace PatternsInAutomatedTests.Advanced.Fluent.BingMainPage
{
    public class BingMainPageElementMap : BasePageElementMap
    {
        public IWebElement SearchBox 
        {
            get
            {
                return this.browser.FindElement(By.Id("sb_form_q"));
            }
        }

        public IWebElement GoButton 
        {
            get
            {
                return this.browser.FindElement(By.Id("sb_form_go"));
            }
        }
       
        public IWebElement ResultsCountDiv
        {
            get
            {
                return this.browser.FindElement(By.Id("b_tween"));
            }
        }

        public IWebElement ImagesLink
        {
            get
            {
                return this.browser.FindElement(By.LinkText("Images"));
            }
        }

        public SelectElement Sizes
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Size']")));
            }
        }

        public SelectElement Color
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Color']")));
            }
        }

        public SelectElement Type
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Type']")));
            }
        }

        public SelectElement Layout
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Layout']")));
            }
        }

        public SelectElement People
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'People']")));
            }
        }

        public SelectElement Date
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'Date']")));
            }
        }

        public SelectElement License
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.XPath("//div/ul/li/span/span[text() = 'License']")));
            }
        }
    }
}