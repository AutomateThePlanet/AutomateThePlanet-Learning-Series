// <copyright file="AmazonPurchaseNullObjectTests.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NullObjectDesignPattern.Core;
using NullObjectDesignPattern.Data;
using NullObjectDesignPattern.Pages.ItemPage;
using NullObjectDesignPattern.Pages.PlaceOrderPage;
using NullObjectDesignPattern.Pages.PreviewShoppingCartPage;
using NullObjectDesignPattern.Pages.ShippingAddressPage;
using NullObjectDesignPattern.Pages.ShippingPaymentPage;
using NullObjectDesignPattern.Pages.SignInPage;

namespace NullObjectDesignPattern
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
                CouponCode = "99PERDIS"
            };
            var clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };

            var purchaseContext = new Base.PurchaseContext(new Strategies.NullPurchasePromotionalCodeStrategy(),
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