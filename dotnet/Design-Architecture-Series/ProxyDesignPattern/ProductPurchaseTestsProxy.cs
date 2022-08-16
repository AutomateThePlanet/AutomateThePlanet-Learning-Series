// Copyright 2022 Automate The Planet Ltd.
// Copyright 2022 Automate The Planet Ltd.
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
using System.Threading;

namespace ProxyDesignPattern;

[TestClass]
public class ProductPurchaseTestsProxy
{
    private IWebDriver _driver;

    [TestInitialize]
    public void TestInitialize()
    {
        _driver = new WebDriverProxy(new ChromeDriver());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _driver.Quit();
    }

    [TestMethod]
    public void CompletePurchaseSuccessfully_WhenNewClientAndWaitProxy()
    {
        _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");

        var addToCartFalcon9 = _driver.FindElement(By.CssSelector("[data-product_id*='28']"));
        addToCartFalcon9.Click();
        ////Thread.Sleep(5000);
        var viewCartButton = _driver.FindElement(By.CssSelector("[class*='added_to_cart wc-forward']"));
        viewCartButton.Click();

        var couponCodeTextField = _driver.FindElement(By.Id("coupon_code"));
        couponCodeTextField.Clear();
        couponCodeTextField.SendKeys("happybirthday");
        var applyCouponButton = _driver.FindElement(By.CssSelector("[value*='Apply coupon']"));
        applyCouponButton.Click();
        ////Thread.Sleep(5000);
        var messageAlert = _driver.FindElement(By.CssSelector("[class*='woocommerce-message']"));
        Assert.AreEqual("Coupon code applied successfully.", messageAlert.Text);

        var quantityBox = _driver.FindElement(By.CssSelector("[class*='input-text qty text']"));
        quantityBox.Clear();
        ////Thread.Sleep(500);
        quantityBox.SendKeys("2");

        ////Thread.Sleep(5000);
        var updateCart = _driver.FindElement(By.CssSelector("[value*='Update cart']"));
        updateCart.Click();
        Thread.Sleep(5000);
        var totalSpan = _driver.FindElement(By.XPath("//*[@class='order-total']//span"));
        Assert.AreEqual("114.00€", totalSpan.Text);

        var proceedToCheckout = _driver.FindElement(By.CssSelector("[class*='checkout-button button alt wc-forward']"));
        proceedToCheckout.Click();

        var billingFirstName = _driver.FindElement(By.Id("billing_first_name"));
        billingFirstName.SendKeys("Anton");
        var billingLastName = _driver.FindElement(By.Id("billing_last_name"));
        billingLastName.SendKeys("Angelov");
        var billingCompany = _driver.FindElement(By.Id("billing_company"));
        billingCompany.SendKeys("Space Flowers");
        var billingCountryWrapper = _driver.FindElement(By.Id("select2-billing_country-container"));
        billingCountryWrapper.Click();
        var billingCountryFilter = _driver.FindElement(By.ClassName("select2-search__field"));
        billingCountryFilter.SendKeys("Germany");
        var germanyOption = _driver.FindElement(By.XPath("//*[contains(text(),'Germany')]"));
        germanyOption.Click();
        var billingAddress1 = _driver.FindElement(By.Id("billing_address_1"));
        billingAddress1.SendKeys("1 Willi Brandt Avenue Tiergarten");
        var billingAddress2 = _driver.FindElement(By.Id("billing_address_2"));
        billingAddress2.SendKeys("Lützowplatz 17");
        var billingCity = _driver.FindElement(By.Id("billing_city"));
        billingCity.SendKeys("Berlin");
        var billingZip = _driver.FindElement(By.Id("billing_postcode"));
        billingZip.Clear();
        billingZip.SendKeys("10115");
        var billingPhone = _driver.FindElement(By.Id("billing_phone"));
        billingPhone.SendKeys("+00498888999281");
        var billingEmail = _driver.FindElement(By.Id("billing_email"));
        billingEmail.SendKeys("info@berlinspaceflowers.com");
        Thread.Sleep(5000);
        var placeOrderButton = _driver.FindElement(By.Id("place_order"));
        placeOrderButton.Click();

        Thread.Sleep(10000);
        var receivedMessage = _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/main/div/header/h1"));
        Assert.AreEqual("Order received", receivedMessage.Text);
    }
}
