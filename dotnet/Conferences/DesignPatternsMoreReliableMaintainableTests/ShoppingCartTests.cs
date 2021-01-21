// <copyright file="ShoppingCartTests.cs" company="Automate The Planet Ltd.">
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

using DesignPatternsMoreReliableMaintainableTests.Base;
using DesignPatternsMoreReliableMaintainableTests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesignPatternsMoreReliableMaintainableTests
{
    [TestClass]
    public class ShoppingCartTests
    {
        private IFactory<ShoppingCart> shoppingCartFactory;
        private ShoppingCart shoppingCart;
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.shoppingCartFactory = new ShoppingCartFactory(this.driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void Purchase_Book_Discounts()
        {
            this.shoppingCart = this.shoppingCartFactory.Create();
            this.shoppingCart.PurchaseItem("The Hitchhiker's Guide to the Galaxy", 22.2, new ClientInfo());
        }
    }
}

// the facade design pattern no facade in the name because you don't want to know that this class is hiding the complexity
// You can use the page objects directly. Because the page objects can be used easily in tests.
/*
 *  hard instanciation to facade
 *  no need to factory to the pages, 
 *  you can change it.
 *  pages are created easily
 */