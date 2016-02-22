using System;
using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Specifications.Base;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public partial class BingMainPage : BasePage
    {
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