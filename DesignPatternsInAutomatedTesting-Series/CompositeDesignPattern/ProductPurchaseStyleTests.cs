// <copyright file="ProductPurchaseStyleTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CompositeDesignPattern
{
     [TestClass]
    public class ProductPurchaseStyleTests
    {
        private IDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new DriverAdapter(new ChromeDriver());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Close();
        }

        [TestMethod]
        public void VerifyStylesOfAddToCartButtons()
        {
            _driver.GoToUrl("http://demos.bellatrix.solutions/");

            var addToCartButtons = _driver.CreateElements(By.XPath("//a[contains(text(),'Add to cart')]"));

            foreach (var addToCartButton in addToCartButtons)
            {
                addToCartButton.AssertFontSize("14px");
                addToCartButton.AssertFontWeight("600");
            }

            addToCartButtons.ForEach(e => e.AssertFontSize("14px"));
            addToCartButtons.ForEach(e => e.AssertFontWeight("600"));

            // composite ElementList
            addToCartButtons.AssertFontSize("14px");
            addToCartButtons.AssertFontWeight("600");
        }

        [TestMethod]
        public void VerifyStylesOfAddToCartButton()
        {
            _driver.GoToUrl("http://demos.bellatrix.solutions/");

            var falcon0AddToCartButton = _driver.Create(By.CssSelector("[data-product_id*='28']"));

            falcon0AddToCartButton.AssertFontSize("14px");
            falcon0AddToCartButton.AssertFontWeight("600");
        }
    }
}
