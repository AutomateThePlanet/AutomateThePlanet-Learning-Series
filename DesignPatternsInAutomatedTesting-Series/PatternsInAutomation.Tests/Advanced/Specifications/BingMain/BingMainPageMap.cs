using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;

namespace PatternsInAutomatedTests.Advanced.Specifications
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
    }
}