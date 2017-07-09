// <copyright file="AmazonPurchase_AdvancedPurchaseStrategy_Tests.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using AdvancedStrategyDesignPattern.Core;
using AdvancedStrategyDesignPattern.Data;
using AdvancedStrategyDesignPattern.Enums;
using AdvancedStrategyDesignPattern.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedStrategyDesignPattern
{
    [TestClass]
    public class AmazonPurchaseAdvancedPurchaseStrategyTests
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
            var itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            var itemPrice = "40.49";
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
            var clientPurchaseInfo = new ClientPurchaseInfo(billingInfo, shippingInfo)
            {
                GiftWrapping = GiftWrappingStyles.Fancy
            };
            var clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };

            new Base.PurchaseContext(new SalesTaxOrderPurchaseStrategy(), new VatTaxOrderPurchaseStrategy(), new GiftOrderPurchaseStrategy())
                                                                                                                                                                           .PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}