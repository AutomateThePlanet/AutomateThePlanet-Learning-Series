using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BitbucketPipelines.Pages
{
    public partial class CheckoutPage
    {
        private const string URL = "https://getbootstrap.com/docs/5.0/examples/checkout/";
        private IWebDriver _driver;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(URL);
        }

        public void FillInfo(ClientInfo clientInfo)
        {
            FirstName.SendKeys(clientInfo.FirstName);
            LastName.SendKeys(clientInfo.LastName);
            Username.SendKeys(clientInfo.Username);
            Email.SendKeys(clientInfo.Email);
            Address1.SendKeys(clientInfo.Address1);
            Address2.SendKeys(clientInfo.Address2);
            Country.SelectByIndex(clientInfo.Country);
            State.SelectByIndex(clientInfo.State);
            Zip.SendKeys(clientInfo.Zip);
            CardName.SendKeys(clientInfo.CardName);
            CardNumber.SendKeys(clientInfo.CardNumber);
            CardExpiration.SendKeys(clientInfo.CardExpiration);
            CardCVV.SendKeys(clientInfo.CardCVV);
            ClickSubmitButton();
        }

        private void ClickSubmitButton()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", SubmitButton);
        }
    }
}
