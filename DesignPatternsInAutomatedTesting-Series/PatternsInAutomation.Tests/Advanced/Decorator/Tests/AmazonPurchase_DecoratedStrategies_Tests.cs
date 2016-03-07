using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Decorator.Advanced.Base;
using PatternsInAutomatedTests.Advanced.Decorator.Advanced.Strategies;
using PatternsInAutomatedTests.Advanced.Decorator.Data;
using PatternsInAutomatedTests.Advanced.Decorator.Enums;

namespace PatternsInAutomatedTests.Advanced.Decorator.Tests
{
    [TestClass]
    public class AmazonPurchase_DecoratedStrategies_Tests
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
        public void Purchase_SeleniumTestingToolsCookbook_DecoratedStrategies()
        {
            string itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            decimal itemPrice = 40.49m;
            var shippingInfo = new ClientAddressInfo()
            {
                FullName = "John Smith",
                Country = "United States",
                Address1 = "950 Avenue of the Americas",
                State = "Texas",
                City = "Houston",
                Zip = "77001",
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
            OrderPurchaseStrategy orderPurchaseStrategy = new TotalPriceOrderPurchaseStrategy(itemPrice);
            orderPurchaseStrategy = new SalesTaxOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, clientPurchaseInfo);
            orderPurchaseStrategy = new VatTaxOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, clientPurchaseInfo);

            new PurchaseContext(orderPurchaseStrategy).PurchaseItem(itemUrl, itemPrice.ToString(), clientLoginInfo, clientPurchaseInfo);
        }
    }
}