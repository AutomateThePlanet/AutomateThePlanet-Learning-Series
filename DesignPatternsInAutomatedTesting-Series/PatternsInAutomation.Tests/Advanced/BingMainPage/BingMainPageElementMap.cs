using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPage
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
    }
}