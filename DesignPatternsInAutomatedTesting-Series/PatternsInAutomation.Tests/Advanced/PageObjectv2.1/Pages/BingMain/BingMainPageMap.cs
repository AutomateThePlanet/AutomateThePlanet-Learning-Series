using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv21
{
    public class BingMainPageMap : BaseElementMap
    {
        public BingMainPageMap(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SearchBox 
        {
            get
            {
                return this.driver.FindElement(By.Id("sb_form_q"));
            }
        }

        public IWebElement GoButton 
        {
            get
            {
                return this.driver.FindElement(By.Id("sb_form_go"));
            }
        }
       
        public IWebElement ResultsCountDiv
        {
            get
            {
                return this.driver.FindElement(By.Id("b_tween"));
            }
        }

        public IWebElement FeelingLuckyButton
        {
            get
            {
                return this.driver.FindElement(By.LinkText("I'm Feeling Lucky"));
            }
        }
    }
}