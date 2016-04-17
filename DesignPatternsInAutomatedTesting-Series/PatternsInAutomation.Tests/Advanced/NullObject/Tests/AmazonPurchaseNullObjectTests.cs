using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.NullObject.Base;
using PatternsInAutomatedTests.Advanced.NullObject.Data;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingPaymentPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.SignInPage;
using PatternsInAutomatedTests.Advanced.NullObject.Strategies;

namespace PatternsInAutomatedTests.Advanced.NullObject.Tests
{
    [TestClass]
    public class AmazonPurchaseNullObjectTests
    { 
        [TestInitialize]
        public void SetupTest()
        {
            Driver.StartBrowser();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_SeleniumTestingToolsCookbook()
        {
            string itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            string itemPrice = "40.49";
            ClientPurchaseInfo clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = "John Smith",
                    Country = "United States",
                    Address1 = "950 Avenue of the Americas",
                    State = "New York",
                    City = "New York City",
                    Zip = "10001-2121",
                    Phone = "00164644885569"
                });
            clientPurchaseInfo.CouponCode = "99PERDIS";
            ClientLoginInfo clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };

            var purchaseContext = new PurchaseContext(new NullPurchasePromotionalCodeStrategy(),
                new ItemPage(Driver.Browser),
                new PreviewShoppingCartPage(Driver.Browser),
                new SignInPage(Driver.Browser),
                new ShippingAddressPage(Driver.Browser),
                new ShippingPaymentPage(Driver.Browser),
                new PlaceOrderPage(Driver.Browser));

            ////var purchaseContext = new PurchaseContext(new UiPurchasePromotionalCodeStrategy(new PlaceOrderPage(Driver.Browser), 20),
            //// new ItemPage(Driver.Browser),
            //// new PreviewShoppingCartPage(Driver.Browser),
            //// new SignInPage(Driver.Browser),
            //// new ShippingAddressPage(Driver.Browser),
            //// new ShippingPaymentPage(Driver.Browser),
            //// new PlaceOrderPage(Driver.Browser));

            purchaseContext.PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}