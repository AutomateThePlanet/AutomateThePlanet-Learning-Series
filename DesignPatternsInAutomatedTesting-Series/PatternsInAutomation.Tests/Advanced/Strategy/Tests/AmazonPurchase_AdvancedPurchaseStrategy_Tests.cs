using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Strategy.Advanced.Base;
using PatternsInAutomatedTests.Advanced.Strategy.Advanced.Strategies;
using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Enums;

namespace PatternsInAutomatedTests.Advanced.Strategy.Tests
{
    [TestClass]
    public class AmazonPurchase_AdvancedPurchaseStrategy_Tests
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
            var shippingInfo = new ClientAddressInfo()
            {
                FullName = "John Smith",
                Country = "United States",
                Address1 = "950 Avenue of the Americas",
                State = "New York",
                City = "New York City",
                Zip = "10001-2121",
                Phone = "00164644885569"
            };
            var billingInfo = new ClientAddressInfo()
            {
                FullName = "Anton Angelov",
                Country = "Bulgaria",
                Address1 = "950 Avenue of the Americas",
                City = "Sofia",
                Zip = "1672",
                Phone = "0894464647"
            };
            ClientPurchaseInfo clientPurchaseInfo = new ClientPurchaseInfo(billingInfo, shippingInfo)
            {
                GiftWrapping = GiftWrappingStyles.Fancy
            };
            ClientLoginInfo clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };

            new PurchaseContext(new SalesTaxOrderPurchaseStrategy(), new VatTaxOrderPurchaseStrategy(), new GiftOrderPurchaseStrategy())
            .PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}