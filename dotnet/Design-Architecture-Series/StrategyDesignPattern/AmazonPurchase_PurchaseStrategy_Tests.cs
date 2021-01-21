// <copyright file="AmazonPurchase_PurchaseStrategy_Tests.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StrategyDesignPattern.Base;
using StrategyDesignPattern.Core;
using StrategyDesignPattern.Data;
using StrategyDesignPattern.Enums;
using StrategyDesignPattern.Strategies;

namespace StrategyDesignPattern
{
    [TestClass]
    public class AmazonPurchasePurchaseStrategyTests
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
            var clientPurchaseInfo = new ClientPurchaseInfo(
            new ClientAddressInfo()
            {
                FullName = "John Smith",
                Country = "United States",
                Address1 = "950 Avenue of the Americas",
                State = "New York",
                City = "New York City",
                Zip = "10001-2121",
                Phone = "00164644885569"
            })
            {
                GiftWrapping = GiftWrappingStyles.None
            };
            var clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };

            new PurchaseContext(new SalesTaxOrderValidationStrategy()).PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}