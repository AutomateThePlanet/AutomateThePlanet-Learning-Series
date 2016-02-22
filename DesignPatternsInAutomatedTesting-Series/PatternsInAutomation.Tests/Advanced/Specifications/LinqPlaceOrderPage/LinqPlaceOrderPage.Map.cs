using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public partial class LinqPlaceOrderPage
    {
        public IWebElement TotalPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[7]/td[2]/strong"));
            }
        }

        public IWebElement EstimatedTaxPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[5]/td[2]"));
            }
        }

        public IWebElement ItemsPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[1]/td[2]"));
            }
        }

        public IWebElement TotalBeforeTaxPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[4]/td[2]"));
            }
        }

        public IWebElement GiftWrapPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement ShippingTax
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement CreditCard
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='creditCard']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement SecurityNumber
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='securityNumber']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement CreditCardOwner
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id=creditCardOwner']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement Wiretransfer
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id=wiretransfer']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement PromotionalCode
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id=promotionalCode']/table/tbody/tr[2]/td[2]"));
            }
        }
    }
}