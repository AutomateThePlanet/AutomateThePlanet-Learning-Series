// <copyright file="ShoppingCartTests.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using TemplateMethodDesignPattern.Base.Second;
using TemplateMethodDesignPattern.Data.Second;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TemplateMethodDesignPattern.Second
{
    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCartFactory _shoppingCartFactory;
        private ShoppingCart _shoppingCart;
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _shoppingCartFactory = new ShoppingCartFactory(_driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void Purchase_Book_Discounts()
        {
            _shoppingCart = _shoppingCartFactory.CreateOldShoppingCart();
            _shoppingCart.PurchaseItem("The Hitchhiker's Guide to the Galaxy", 22.2, new ClientInfo());
        }
    }
}