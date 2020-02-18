// <copyright file="ProductPurchaseTestsAdapter.cs" company="Automate The Planet Ltd.">
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

namespace LazyLoadingDesignPattern
{
     [TestClass]
    public class ProductPurchaseTestsAdapter
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
        public void CompletePurchaseSuccessfully_WhenNewClient()
        {
            _driver.GoToUrl("http://demos.bellatrix.solutions/");

            var addToCartFalcon9 = _driver.Create(By.CssSelector("[data-product_id*='28']"));
            var viewCartButton = _driver.Create(By.CssSelector("[class*='added_to_cart wc-forward']"));
            var couponCodeTextField = _driver.Create(By.Id("coupon_code"));
            var applyCouponButton = _driver.Create(By.CssSelector("[value*='Apply coupon']"));
            var messageAlert = _driver.Create(By.CssSelector("[class*='woocommerce-message']"));
            var quantityBox = _driver.Create(By.CssSelector("[class*='input-text qty text']"));
            var totalSpan = _driver.Create(By.XPath("//*[@class='order-total']//span"));
            var proceedToCheckout = _driver.Create(By.CssSelector("[class*='checkout-button button alt wc-forward']"));
            var billingFirstName = _driver.Create(By.Id("billing_first_name"));
            var billingCountryWrapper = _driver.Create(By.Id("select2-billing_country-container"));
            var billingCountryFilter = _driver.Create(By.ClassName("select2-search__field"));
            var updateCart = _driver.Create(By.CssSelector("[value*='Update cart']"));
            var germanyOption = _driver.Create(By.XPath("//*[contains(text(),'Germany')]"));
            var billingAddress1 = _driver.Create(By.Id("billing_address_1"));
            var billingAddress2 = _driver.Create(By.Id("billing_address_2"));
            var billingCity = _driver.Create(By.Id("billing_city"));
            var billingZip = _driver.Create(By.Id("billing_postcode"));
            var billingLastName = _driver.Create(By.Id("billing_last_name"));
            var billingCompany = _driver.Create(By.Id("billing_company"));
            var billingPhone = _driver.Create(By.Id("billing_phone"));
            var billingEmail = _driver.Create(By.Id("billing_email"));
            var placeOrderButton = _driver.Create(By.Id("place_order"));
            var receivedMessage = _driver.Create(By.XPath("/html/body/div[1]/div/div/div/main/div/header/h1"));

            addToCartFalcon9.Click();
            viewCartButton.Click();
            couponCodeTextField.TypeText("happybirthday");
            applyCouponButton.Click();
           
            Assert.AreEqual("Coupon code applied successfully.", messageAlert.Text);
          
            quantityBox.TypeText("2");
            updateCart.Click();
            _driver.WaitForAjax();
            
            Assert.AreEqual("114.00€", totalSpan.Text);
           
            proceedToCheckout.Click();
            
            billingFirstName.TypeText("Anton");
            billingLastName.TypeText("Angelov");
            billingCompany.TypeText("Space Flowers");
            billingCountryWrapper.Click();
            billingCountryFilter.TypeText("Germany");
            germanyOption.Click();
            billingAddress1.TypeText("1 Willi Brandt Avenue Tiergarten");
            billingAddress2.TypeText("Lützowplatz 17");
            billingCity.TypeText("Berlin");
            billingZip.TypeText("10115");
            billingPhone.TypeText("+00498888999281");
            billingEmail.TypeText("info@berlinspaceflowers.com");
            _driver.WaitForAjax();
            placeOrderButton.Click();
            _driver.WaitForAjax();
           
            Assert.AreEqual("Order received", receivedMessage.Text);
        }
    }
}
